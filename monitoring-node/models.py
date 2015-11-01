
class Behavior:
    def __init__(self):
        self.name = 'no name'
        self.nodes = []


class Node:
    def __init__(self):
        self.behavior = None
        self.physical_address = None
        self.sensors = []


class Sensor:
    def __init__(self):
        self.node = None
        self.sensor_name = None
        self.measurement_name = None
        self.measurement_unit = None
        self.readings = []

    def __unicode__(self):
        return '{} {}'.format(self.id, self.sensor_name)

class SensorReading:
    def __init__(self):
        self.sensor = None
        self.reading = None
        self.reading_date = None

    def __unicode__(self):
            return '{}'.format(self.reading)
