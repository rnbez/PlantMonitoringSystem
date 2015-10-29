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
        sensor = validated_data.pop('sensor')
        sensor = Sensor.objects.get(sensor_name=sensor['sensor_name'])
        reading = SensorReading.objects.create(sensor=sensor, **validated_data)
         sensor.readings.add(reading)
        return reading
    def update(self, instance, validated_data):
        instance.reading = validated_data.get('reading', instance.reading)
        instance.reading_date = validated_data.get('reading_date', instance.reading_date)
        instance.save()
        return instance
