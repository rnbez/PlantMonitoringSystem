import random

class SensorReader:

    @staticmethod
    def read_soil_temperature():
        return random.randint(10, 50)

    @staticmethod
    def read_air_temperature():
        return random.randint(15, 40)

    @staticmethod
    def read_moisture():
        return random.randint(0, 1023)

    @staticmethod
    def read_luminosity():
        return random.randint(0, 1023)
