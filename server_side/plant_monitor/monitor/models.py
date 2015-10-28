from django.db import models
#from django.utils import timezone

class Behavior(models.Model):
    """docstring for Behavior"""
    name = models.CharField(max_length=200)


class Node(models.Model):
    """docstring for Node"""
    behavior = models.ForeignKey(Behavior)
    physical_address = models.CharField(max_length=200)


class Sensor(models.Model):
    """docstring for Sensor"""
    node = models.ForeignKey(Node)
    sensor_name = models.CharField(max_length=200)
    measurement_name = models.CharField(max_length=200)
    measurement_unit = models.CharField(max_length=50)


class SensorReading(models.Model):
    """docstring for SensorReading"""
    sensor = models.ForeignKey(Sensor)
    reading = models.IntegerField(default=0)
    reading_date = models.DateTimeField('date and time of the reading')
