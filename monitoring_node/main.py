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

def updateNode(nextRun):
    if datetime.now() >= nextRun:
	print 'Start updating the node...'
        response = httpclient.get(api.__get_node__(node.node_info['id']))
        body = json.loads(response.body)
        node.update(body)
        nextRun = datetime.now() + timedelta(seconds=60)
	print 'Node update finised\n'

    return nextRun

def doActions():
    try:
        response = httpclient.get(api.__get_lightwater__(node.node_info['id']))
        _node = json.loads(response.body)
        actions.setPin(actions.__light_pin__, _node['lightOn'])
        actions.setPin(actions.__water_pin__, _node['waterOn'])
    except TimeoutError:
	error_msg = "HTTP Timeout - main.py @ doActions()"
	print error_msg, "\n"
	log.log_error(error_msg)
	return
    return

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
        error_msg = error_msg + "\n" + str(e)
        log.log_error(error_msg)

    print "\n----------------------\n"
    actions.setup()
    print "GPIO Setup"
    print "\n----------------------\n"

    nextSensorReading = datetime.now()
    nextNodeUpdate = datetime.now()
    delay = 0.1
    while True:
        try:
            nextSensorReading = sendReadings(nextSensorReading)
            doActions()
	    time.sleep(delay)

            nextNodeUpdate = updateNode(nextNodeUpdate)
            doActions()
	    time.sleep(delay)
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
            time.sleep(15)
