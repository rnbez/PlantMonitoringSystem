from sensors import *
from datetime import datetime, timedelta
import sys, time, httpclient, node, api, json, log, actions, auth


def sendReadings(nextRun):

    if datetime.now() >= nextRun:
        log.current_state = "Reading Air Temperature"
        air_temp = DHT11TemperatureSensor.read()
        log.current_state = "Posting Air Temperature"
        httpclient.post(api.__send_readings__, air_temp)

        log.current_state = "Reading Air Humidity"
        air_hum = DHT11HumiditySensor.read()
        log.current_state = "Posting Air Humidity"
        httpclient.post(api.__send_readings__, air_hum)

        log.current_state = "Reading Luminosity"
        air_lum = LDR.read()
        log.current_state = "Posting Luminosity"
        httpclient.post(api.__send_readings__, air_lum)

        log.current_state = "Reading Soil Temperature"
        soil_temp = DS18B20TemperatureSensor.read()
        log.current_state = "Reading Soil Temperature"
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
        log.current_state = "Getting node info from server"
        response = httpclient.get(api.__get_node__(node.node_info['id']))
        log.current_state = "Deserializing node info"
        body = json.loads(response.body)
        log.current_state = "Updating node info"
        node.update(body)
        nextRun = datetime.now() + timedelta(seconds=60)
	print 'Node update finised\n'

    return nextRun

def doActions():
    try:
        log.current_state = "Getting light and water info from server"
        response = httpclient.get(api.__get_lightwater__(node.node_info['id']))
        log.current_state = "Deserializing light and water info from server"
        _node = json.loads(response.body)
        log.current_state = "Setting light pin"
        actions.setPin(actions.__light_pin__, _node['lightOn'])
        log.current_state = "Setting water pin"
        actions.setPin(actions.__water_pin__, _node['waterOn'])
    except TimeoutError:
	error_msg = "HTTP Timeout - main.py @ doActions()"
	print error_msg, "\n"
	log.log_error(error_msg)
	return
    return

if __name__ == '__main__':

    # authentication
    try:
        log.current_state = "Ready to authenticate"
        auth.authenticate()
        log.current_state = "Authenticated"
        print "authentication succeeded"
    except:
        error_msg = "authentication fail"
        e = sys.exc_info()[0]
        print error_msg
        error_msg = "\n" + error_msg + "\n" + str(e)
        log.log_error(error_msg)

    #handshake

    try:

        log.current_state = "Ready to do the handshake"
        response = httpclient.post(api.__handshake__, node.get())
        print response.status, response.reason
        if response.status == 200:
            #print response.body
            body = json.loads(response.body)
            node.update(body)

            log.current_state = "Handshake succeeded"
            print "handshake succeeded"
    except:
        error_msg = "handshake fail"
        e = sys.exc_info()[0]
        print error_msg
        error_msg = "\n" + error_msg + "\n" + str(e)
        log.log_error(error_msg)

    print "\n----------------------\n"
    log.current_state = "Ready to setup the GPIO pins"
    actions.setup()
    print "GPIO Setup"
    log.current_state = "GPIO pins set"
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
