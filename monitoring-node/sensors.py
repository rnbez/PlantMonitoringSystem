import random
import datetime

class SensorReader:

    @staticmethod
    def build_reading(sensor, reading):
        date = datetime.datetime.now().isoformat()
        return {"reading":reading,
                "date": date,
                "sensor": sensor}

    @staticmethod
    def read_soil_temperature():
        return random.randint(10, 50)

    @staticmethod
    def read_air_temperature():
        return random.randint(15, 40)

    @staticmethod
    def read_air_humidity():
        return random.randint(20, 80)

    @staticmethod
    def read_moisture():
        return random.randint(0, 100)

    @staticmethod
    def read_luminosity():
        return random.randint(0, 100)
