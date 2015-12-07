from sensors import *
from datetime import datetime, timedelta
import screen as scn
import sys, time, httpclient, node, api, json, log, sys_gpio, auth


def sendReadings(nextRun):

    if datetime.now() >= nextRun:

        #turn on the sensors
        # the transisotr enable the sensors to be powered
        sys_gpio.setTransistor(True)

        log.current_state = "Reading Air Temperature"
        air_temp = DHT11TemperatureSensor.read()
        log.current_state = "Posting Air Temperature"
        httpclient.post(api.__send_readings__, air_temp, auth.checkResponse)
        scn.update(env_temp=str(air_temp['reading']))

        log.current_state = "Reading Air Humidity"
        air_hum = DHT11HumiditySensor.read()
        log.current_state = "Posting Air Humidity"
        httpclient.post(api.__send_readings__, air_hum, auth.checkResponse)
        scn.update(env_hum=str(air_hum['reading']))

        log.current_state = "Reading Luminosity"
        air_lum = LDR.read()
        log.current_state = "Posting Luminosity"
        httpclient.post(api.__send_readings__, air_lum, auth.checkResponse)
        scn.update(env_lum="{0:0.2f}".format(air_lum['reading']))

        log.current_state = "Reading Soil Temperature"
        soil_temp = DS18B20TemperatureSensor.read()
        log.current_state = "Reading Soil Temperature"
        httpclient.post(api.__send_readings__, soil_temp, auth.checkResponse)
        scn.update(soil_temp="{0:0.2f}".format(soil_temp['reading']))

        sys_gpio.setTransistor(False)

        #print air_lum["date"]
        #print "Env: Temp.: {0:0.2f}C  Humidity: {1:0.2f}%  &  Luminosity:{2:0.2f}%".format(air_temp['reading'], air_hum['reading'], air_lum['reading'])
        #print "Soil: Temp.: {0:0.2f}C  &  Moisture: 0%".format(soil_temp['reading'])
        #print "\n"
        log.log_info("System Ok")

        nextRun = datetime.now() + timedelta(seconds=60)

    return nextRun

def updateNode(nextRun):
    if datetime.now() >= nextRun:
        #print 'Start updating the node...'
        log.current_state = "Getting node info from server"
        scn.update(node_updating="Updating node info [Calling Server]")

        response = httpclient.get(api.__get_node__(node.node_info['id']), auth.checkResponse)
        scn.update(server_resp="{} {}".format(response.status, response.reason))

        log.current_state = "Deserializing node info"
        body = json.loads(response.body)
        log.current_state = "Updating node info"
        scn.update(node_updating="Updating node info [Saving Info]")

        node.update(body)
        scn.update(node_updating="Last update at " + datetime.now().isoformat())

        nextRun = datetime.now() + timedelta(seconds=60)
        #print 'Node update finised\n'

    return nextRun

def doActions():
    try:
        log.current_state = "Getting light and water info from server"
        response = httpclient.get(api.__get_lightwater__(node.node_info['id']), auth.checkResponse)
        scn.update(server_resp="{} {}".format(response.status, response.reason))
        log.current_state = "Deserializing light and water info from server"
        _node = json.loads(response.body)
        log.current_state = "Setting light pin"
        sys_gpio.setRelay(sys_gpio.__light_pin__, _node['lightOn'])
        log.current_state = "Setting water pin"
        sys_gpio.setRelay(sys_gpio.__water_pin__, _node['waterOn'])
    except TimeoutError:
    	error_msg = "HTTP Timeout - main.py @ doActions()"
    	#print error_msg, "\n"
        scn.update(last_error="Get Water and Light - at " + datetime.now().isoformat())
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
        error_msg = "authentication failed"
        e = sys.exc_info()[0]
        print error_msg
        error_msg = "\n" + error_msg + "\n" + str(e)
        log.log_error(error_msg)

    #handshake
    try:
        new_node = False;
        while True:
            user_input = raw_input("Is this a new node[y/N]?")
            if user_input is "":
                user_input = "n"
            user_input = user_input.lower()
            if user_input == "y" or user_input == "n":
                if user_input == "y":
                    new_node = True
                else:
                    new_node = False
                break

        log.current_state = "Ready to do the handshake"
        response = httpclient.post(api.__handshake__, node.get(new_node, auth.__user_id__), auth.checkResponse)
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

    try:
        print "\n----------------------\n"
        log.current_state = "Ready to setup the GPIO pins"
        sys_gpio.setup()
        print "GPIO Setup Succeeded"
        log.current_state = "GPIO pins set"
        print "\n----------------------\n"
    except Exception as e:
        print "GPIO Setup Error"
        print "GPIO Clean"
        sys_gpio.clean()
        print "GPIO Setup Succeeded"
        sys_gpio.setup()


    scn.update()
    scn.update(soil_moist = str(0)) # remove later


    nextSensorReading = datetime.now()
    nextNodeUpdate = datetime.now()
    delay = 0.25
    while True:
        try:
            nextSensorReading = sendReadings(nextSensorReading)
            nextNodeUpdate = updateNode(nextNodeUpdate)
            doActions()
            scn.update()
            time.sleep(delay)
        except KeyboardInterrupt:
            scn.end()
            sys_gpio.clean()
            log.log_info("GPIO CleanUp")
            log.log_info("User Interrupt")
            exit()
        except:
            error_msg = "Unexpected error"
            e = sys.exc_info()[0]
            #print error_msg
            error_msg = error_msg + str(e)
            log.log_error(error_msg)
            scn.update(last_error="Unexpected Error - at " + datetime.now().isoformat())
            try:
                time.sleep(15)
            except KeyboardInterrupt:
                scn.end()
                sys_gpio.clean()
                log.log_info("GPIO CleanUp")
                log.log_info("User Interrupt")
                exit()
