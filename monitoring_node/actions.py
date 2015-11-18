import RPi.GPIO as GPIO

__light_pin__ = 24
__water_pin__ = 25

def setup():
    global __light_pin__
    global __water_pin__
    GPIO.setmode(GPIO.BCM)
    GPIO.setup(__light_pin__, GPIO.OUT)
    GPIO.setup(__water_pin__, GPIO.OUT)
    return

def setPin(pin, value):
    GPIO.output(18, value)
    return

def clean():
    GPIO.cleanup()
    return
