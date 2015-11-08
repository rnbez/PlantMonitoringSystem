from sensors import *
import time, httpclient, node, api, json


if __name__ == '__main__':

    #handshake

    response = httpclient.post(api.__handshake__, node.get())
    print response.status, response.reason
    if response.status == 200:
        print response.body
        body = json.loads(response.body)
        node.update(body)

    while True:
        try:
            air_temp = SensorReader.read_air_temperature()
            httpclient.post(api.__send_readings__, air_temp)

            air_hum = SensorReader.read_air_humidity()
            httpclient.post(api.__send_readings__, air_hum)

            air_lum = SensorReader.read_luminosity()
            httpclient.post(api.__send_readings__, air_lum)

            print "Env: Temp.: {}C  Humidity: {}%  &  Luminosity:{}%".format(str(air_temp['reading']), str(air_hum['reading']), str(air_lum['reading']))

            soil_temp = SensorReader.read_soil_temperature()
            httpclient.post(api.__send_readings__, soil_temp)

            moisture = SensorReader.read_moisture()
            httpclient.post(api.__send_readings__, moisture)
            print "Soil: Temp.: {}C  &  Moisture:{}".format(str(soil_temp['reading']), str(moisture['reading']))
            print "\n"


            time.sleep(5)
        except KeyboardInterrupt:
            exit()
