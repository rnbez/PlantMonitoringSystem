from sensors import *
import time, httpclient, node, api, json


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
        print 'handshake fail'

    print "\n----------------------\n"

    while True:
        try:
            air_temp = DHT11TemperatureSensor.read()
            httpclient.post(api.__send_readings__, air_temp)

            air_hum = DHT11HumiditySensor.read()
            httpclient.post(api.__send_readings__, air_hum)

            air_lum = LDR.read()
            httpclient.post(api.__send_readings__, air_lum)

            soil_temp = DS18B20TemperatureSensor.read()
            httpclient.post(api.__send_readings__, soil_temp)

            print "Env: Temp.: {0:0.2f}C  Humidity: {1:0.2f}%  &  Luminosity:{2:0.2f}%".format(air_temp['reading'], air_hum['reading'], air_lum['reading'])
            print "Soil: Temp.: {0:0.2f}C  &  Moisture: 0%".format(soil_temp['reading'])
            print "\n"


            time.sleep(2)
        except KeyboardInterrupt:
            exit()
