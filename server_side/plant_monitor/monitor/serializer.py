from rest_framework import serializers
from models import Behavior, Node, Sensor, SensorReading

class BehaviorSerializer(serializers.ModelSerializer):
    class Meta:
        model = Behavior
        field = ('id', 'name')

class NodeSerializer(serializers.ModelSerializer):
    behavior = BehaviorSerializer()
    class Meta:
        model = Node
        field = ('id', 'physical_address', 'behavior')

class SensorSerializer(serializers.ModelSerializer):
    node = NodeSerializer()
    #readings = SensorReadingSerializer(many=True, read_only=True)
    class Meta:
        model = Sensor
        field = ('id', 'sensor_name', 'measurement_name', 'measurement_unit', 'node')

class SensorReadingSerializer(serializers.ModelSerializer):
    sensor = SensorSerializer()
    class Meta:
        model = SensorReading
        field = ('id', 'reading', 'reading_date', 'sensor')
        depth = 1
        #field = ('id', 'reading', 'reading_date')

    def create(self, validated_data):
        print '\n\nDEBUG\n\n'
        sensor = validated_data.pop('sensor')
        sensor = Sensor.objects.get(sensor_name=sensor['sensor_name'])
        print sensor
        reading = SensorReading.objects.create(sensor=sensor, **validated_data)
        print reading
        sensor.readings.add(reading)
        return reading
