import RPi.GPIO as GPIO

__transitor_pin__ = 23
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
    __pin_status__[__light_pin__] = True
    __pin_status__[__water_pin__] = True
    GPIO.output(__light_pin__, __pin_status__[__light_pin__])
    GPIO.output(__water_pin__, __pin_status__[__water_pin__])

    # setting transistor
    GPIO.setup(__transitor_pin__, GPIO.OUT)
    GPIO.output(__transitor_pin__, False)
    return

def setRelay(pin, value):
    global __pin_status__

    #if pin in __pin_status__ and __pin_status__[pin] != value:
    # the next line used the not to change the 'value' parameter
    # this happens becauses the pins are connected to a relay
    GPIO.output(pin, (not value))

    return

def setTransistor(value):
    global __transitor_pin__
    GPIO.output(__transitor_pin__, value)

def clean():
    GPIO.cleanup()
    return
