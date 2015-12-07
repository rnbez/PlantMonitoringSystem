from datetime import datetime, timedelta
import sys, time, json
import screen as scn
import httpclient, api, auth, log#, sys_gpio

__current_behavior__ = {}

def getBehavior():
    global __current_behavior__

    __current_behavior__ = {"id":1,
            "name": "Default",
            "waterAuto": True,
            "waterTimeEvery": 5,
            "waterHumLevel": 35.0,
            "humidityAverage": 32.8,
            "lightAuto": True,
            "lightStartHour": 5,
            "lightStopHour": 5,
            "lightLumLevel": 50.0,
            "luminosityAverage": 51.8

            }
    return __current_behavior__

def runBehavior():
    global __current_behavior__

    bhr = __current_behavior__

    if bhr["lightAuto"]:
        current_hour = datetime.now().hour
        if bhr["lightStartHour"] <= current_hour and bhr["lightStopHour"] > current_hour:
            sys_gpio.setRelay(sys_gpio.__light_pin__, True)
            #print "Ligth On"
        else:
            sys_gpio.setRelay(sys_gpio.__light_pin__, False)
            #print "Ligth Off"



    if bhr["waterAuto"]:
        if bhr["waterHumLevel"] not 0:
            if bhr["humidityAverage"] < bhr["waterHumLevel"]:
                sys_gpio.setRelay(sys_gpio.__water_pin__, True)
                #print "Water On"
            else:
                sys_gpio.setRelay(sys_gpio.__water_pin__, False)
                #print "Water Off"






#def doManualActions():
#    try:
#        log.current_state = "Getting light and water info from server"
#        response = httpclient.get(api.__get_lightwater__(node.node_info['id']), auth.checkResponse)
#        scn.update(server_resp="{} {}".format(response.status, response.reason))
#        log.current_state = "Deserializing light and water info from server"
#        _node = json.loads(response.body)
#        log.current_state = "Setting light pin"
#        sys_gpio.setRelay(sys_gpio.__light_pin__, _node['lightOn'])
#        log.current_state = "Setting water pin"
#        sys_gpio.setRelay(sys_gpio.__water_pin__, _node['waterOn'])
#    except TimeoutError:
#    	error_msg = "HTTP Timeout - main.py @ doActions()"
#    	#print error_msg, "\n"
#        scn.update(last_error="Get Water and Light - at " + datetime.now().isoformat())
#    	log.log_error(error_msg)
#    	return
#    return
