import random, datetime, node
#DHT SENSOR
import Adafruit_DHT
#DS18B20 SENSOR
from w1thermsensor import W1ThermSensor
#LDR (LUMINOSITY SENSOR)
import spidev



class ReadingBuilder:
    @staticmethod
    def get(sensor_id, reading):
        date = datetime.datetime.now().isoformat()
        return {"reading":reading,
                "date": date,
                "sensor": sensor_id}

class DHT11TemperatureSensor:
    sensor_type = 'dht11_temperature'
    pin = 18
    sensor = Adafruit_DHT.DHT11

    @staticmethod
    def read():
        sensor_type = DHT11TemperatureSensor.sensor_type
        sensors = node.node_info['sensors']
        found = [sen['id'] for sen in sensors if sen['sensorType'] == sensor_type]

        if not found:
            return None
        else:
            humidity, temperature = Adafruit_DHT.read_retry(sensor, pin)
            return ReadingBuilder.get(found[0], temperature)

class DHT11HumiditySensor:
    sensor_type = 'dht11_humidity'
    pin = 18
    sensor = Adafruit_DHT.DHT11

    @staticmethod
    def read():
        sensor_type = DHT11HumiditySensor.sensor_type
        sensors = node.node_info['sensors']
        found = [sen['id'] for sen in sensors if sen['sensorType'] == sensor_type]

        if not found:
            return None
        else:
            humidity, temperature = Adafruit_DHT.read_retry(sensor, pin)
            return ReadingBuilder.get(found[0], humidity)

class DS18B20TemperatureSensor:
    sensor_type = 'ds18b20'

    @staticmethod
    def read():
        sensor_type = DS18B20TemperatureSensor.sensor_type
        sensors = node.node_info['sensors']
        found = [sen['id'] for sen in sensors if sen['sensorType'] == sensor_type]

        if not found:
            return None
        else:
            thermSensor = W1ThermSensor()
            temperature = thermSensor.get_temperature(W1ThermSensor.DEGREES_C)
            temperature = 1
            return ReadingBuilder.get(found[0], temperature)


class LDR:
    sensor_type = 'ds18b20'
    # channel on the ADC MPC8003
    light_channel = 0

    # Open SPI bus
    spi = spidev.SpiDev()
    spi.open(0,0)
    @staticmethod
    def ReadChannel(channel):
        adc = spi.xfer2([1,(8+channel)<<4,0])
        data = ((adc[1]&3) << 8) + adc[2]
        #scaling from 0-1023 to 1-100
        data = ((data * 100) / float(1023)) + 1
        return data

    @staticmethod
    def read():
        sensor_type = DS18B20TemperatureSensor.sensor_type
        sensors = node.node_info['sensors']
        found = [sen['id'] for sen in sensors if sen['sensorType'] == sensor_type]

        if not found:
            return None
        else:
            light_level = ReadChannel(light_channel)
            return ReadingBuilder.get(found[0], light_level)
