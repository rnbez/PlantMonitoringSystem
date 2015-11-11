import random, datetime

class SensorReader:

    @staticmethod
    def build_reading(sensor_id, reading):
        date = datetime.datetime.now().isoformat()
        return {"reading":reading,
                "date": date,
                "sensor": sensor_id}

    @staticmethod
    def read_air_temperature():
        data = 15 + (random.random() % 40)
        return SensorReader.build_reading(1, data)

    @staticmethod
    def read_air_humidity():
        data = 20 + (random.random() % 80)
        return SensorReader.build_reading(2, data)

    @staticmethod
    def read_luminosity():
        data = (random.random() % 100)
        return SensorReader.build_reading(3, data)

    @staticmethod
    def read_moisture():
        data = (random.random() % 100)
        return SensorReader.build_reading(4, data)

    @staticmethod
    def read_soil_temperature():
        data = 10 + (random.random() % 100)
        return SensorReader.build_reading(5, data)
