from sensors import *
from datetime import datetime, timedelta
import sys, time, httpclient, node, api, json, log, actions


def sendReadings(nextRun):

    if datetime.now() >= nextRun:
        air_temp = DHT11TemperatureSensor.read()
        httpclient.post(api.__send_readings__, air_temp)

        air_hum = DHT11HumiditySensor.read()
        httpclient.post(api.__send_readings__, air_hum)

        air_lum = LDR.read()
        httpclient.post(api.__send_readings__, air_lum)

        soil_temp = DS18B20TemperatureSensor.read()
        httpclient.post(api.__send_readings__, soil_temp)

        print air_lum["date"]
        print "Env: Temp.: {0:0.2f}C  Humidity: {1:0.2f}%  &  Luminosity:{2:0.2f}%".format(air_temp['reading'], air_hum['reading'], air_lum['reading'])
        print "Soil: Temp.: {0:0.2f}C  &  Moisture: 0%".format(soil_temp['reading'])
        print "\n"
        log.log_info("System Ok")

        nextRun = datetime.now() + timedelta(seconds=60)

    return nextRun

def getNodeFromServer():
    return json.loads(httpclient.get(api.__get_node__(node.node_info['id'])).body)

def doActions(_node):
    #print 'lightOn', _node['lightOn'], '\n'
    #print 'waterOn', _node['waterOn'], '\n'
    actions.setPin(actions.__light_pin__, _node['lightOn'])
    actions.setPin(actions.__water_pin__, _node['waterOn'])

    return 1
    # check if the light property
    # in the param node is True
    #     if true turn on the light
    #     if not turn off the light

if __name__ == '__main__':

    #handshake

    try:
        response = httpclient.post(api.__handshake__, node.get())
        print response.status, response.reason
        if response.status == 200:
            print response.body
            body = json.loads(response.body)
            node.update(body)
    except:
        error_msg = "handshake fail"
        e = sys.exc_info()[0]
        print error_msg
        error_msg = error_msg + str(e)
        log.log_error(error_msg)

    print "\n----------------------\n"
    actions.setup()
    print "GPIO Setup"
    print "\n----------------------\n"

    nextRun = datetime.now()
    while True:
        try:

            nextRun = sendReadings(nextRun)
            updatedNode = getNodeFromServer()
            doActions(updatedNode)



            time.sleep(1)
        except KeyboardInterrupt:
            actions.clean()
            log.log_info("GPIO CleanUp")
            log.log_info("User Interrupt")
            exit()
        except:
            error_msg = "Unexpected error"
            e = sys.exc_info()[0]
            print error_msg
            error_msg = error_msg + str(e)
            log.log_error(error_msg)
            time.sleep(60)
