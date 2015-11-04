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
        data = random.randint(15, 40)
        return SensorReader.build_reading(1, data)

    @staticmethod
    def read_air_humidity():
        data = random.randint(20, 80)
        return SensorReader.build_reading(2, data)

    @staticmethod
    def read_luminosity():
        data = random.randint(0, 100)
        return SensorReader.build_reading(3, data)

    @staticmethod
    def read_moisture():
        data = random.randint(0, 100)
        return SensorReader.build_reading(4, data)

    @staticmethod
    def read_soil_temperature():
        data = random.randint(10, 50)
        return SensorReader.build_reading(5, data)
