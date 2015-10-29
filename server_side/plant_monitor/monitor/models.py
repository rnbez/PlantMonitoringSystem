from django.db import models
#from django.utils import timezone

class Behavior(models.Model):
    """docstring for Behavior"""
    name = models.CharField(max_length=200)


class Node(models.Model):
    """docstring for Node"""
    behavior = models.ForeignKey(Behavior, related_name='nodes')
    physical_address = models.CharField(max_length=200)


class Sensor(models.Model):
    """docstring for Sensor"""
    node = models.ForeignKey(Node, related_name='sensors')
    sensor_name = models.CharField(max_length=200)
    measurement_name = models.CharField(max_length=200)
    measurement_unit = models.CharField(max_length=50)

    def __unicode__(self):
        return '{} {}'.format(self.id, self.sensor_name)

class SensorReading(models.Model):
    """docstring for SensorReading"""
    sensor = models.ForeignKey(Sensor, related_name='readings')
    reading = models.IntegerField(default=0)
    reading_date = models.DateTimeField('date and time of the reading')

    def __unicode__(self):
            return '{}'.format(self.reading)
