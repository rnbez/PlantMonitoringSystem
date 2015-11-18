import RPi.GPIO as GPIO

__light_pin__ = 24
__water_pin__ = 25
__pin_status__ = {}

def setup():
    global __light_pin__
    global __water_pin__
    global __pin_status__

    GPIO.setmode(GPIO.BCM)
    GPIO.setup(__light_pin__, GPIO.OUT)
    GPIO.setup(__water_pin__, GPIO.OUT)
    __pin_status__[__light_pin__] = False
    __pin_status__[__water_pin__] = False
    GPIO.output(__light_pin__, __pin_status__[__light_pin__])
    GPIO.output(__water_pin__, __pin_status__[__water_pin__])
    return

def setPin(pin, value):
    global __pin_status__

    #if pin in __pin_status__ and __pin_status__[pin] != value:
    GPIO.output(pin, value)

    return

def clean():
    GPIO.cleanup()
    return
