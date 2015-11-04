from sensors import *
import time, httpclient, node

if __name__ == '__main__':

    #handshake
    response = httpclient.post(PATH_NODE_GREETING, node.get())
    if response.status == 200
        data = response.read()
        node.update(data)

    while True:
        try:
            air_temp = SensorReader.read_air_temperature()
            httpclient.post(httpclient.PATH_READING, air_temp)

            air_hum = SensorReader.read_air_humidity()
            httpclient.post(httpclient.PATH_READING, air_hum)

            air_lum = SensorReader.read_luminosity()
            httpclient.post(httpclient.PATH_READING, air_lum)

            print "Env: Temp.: {}C  Humidity: {}%  &  Luminosity:{}%".format(str(air_temp['reading']), str(air_hum['reading']), str(air_lum['reading']))

            soil_temp = SensorReader.read_soil_temperature()
            httpclient.post(httpclient.PATH_READING, soil_temp)

            moisture = SensorReader.read_moisture()
            httpclient.post(httpclient.PATH_READING, moisture)
            print "Soil: Temp.: {}C  &  Moisture:{}".format(str(soil_temp['reading']), str(moisture['reading']))
            print "\n"


            time.sleep(5)
        except KeyboardInterrupt:
            exit()
