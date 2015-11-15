from w1thermsensor import W1ThermSensor
import time

sensor = W1ThermSensor()

while True:
    celsius = sensor.get_temperature(W1ThermSensor.DEGREES_C)
    fahrenheit = sensor.get_temperature(W1ThermSensor.DEGREES_F)
    print 'Celsius={0:0.2f}C  Fahrenheit={1:0.2f}F'.format(celsius, fahrenheit)
    time.sleep(2)
