from django.http import Http404
from rest_framework.decorators import api_view, permission_classes
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework.permissions import AllowAny
from rest_framework import status
from serializer import *
from models import *


"""""""""""
__________
NODE VIEWS
"""""""""""
class NodeList(APIView):
    permission_classes = (AllowAny,)

    def get(self, request, format=None):
        question_set = Node.objects.all()
        serialized = NodeSerializer(question_set, many=True)
        return Response(serialized.data)

    def post(self, request, format=None):
        serializer = NodeSerializer(data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

class NodeDetail(APIView):
    permission_classes = (AllowAny,)

    def get_object(self, pk):
        try:
            return Node.objects.get(pk=pk)
        except Node.DoesNotExist:
            raise Http404

    def get(self, request, pk, format=None):
        node = self.get_object(pk)
        serialized = NodeSerializer(node)
        return Response(serialized.data)

    def put(self, request, pk, format=None):
        node = self.get_object(pk)
        serializer = NodeSerializer(node, data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

    def delete(self, request, pk, format=None):
        node = self.get_object(pk)
        node.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)


"""""""""""""""
____________
SENSOR VIEWS
"""""""""""""""
class SensorList(APIView):
    permission_classes = (AllowAny,)

    def get(self, request, format=None):
        question_set = Sensor.objects.all()
        serialized = SensorSerializer(question_set, many=True)
        return Response(serialized.data)

    def post(self, request, format=None):
        serializer = SensorSerializer(data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

class SensorDetail(APIView):
    permission_classes = (AllowAny,)

    def get_object(self, pk):
        try:
            return Sensor.objects.get(pk=pk)
        except Sensor.DoesNotExist:
            raise Http404

    def get(self, request, pk, format=None):
        sensor = self.get_object(pk)
        serialized = SensorSerializer(sensor)
        return Response(serialized.data)



"""""""""""""""""""""""
____________________
SENSOR READING VIEWS
"""""""""""""""""""""""
class SensorReadingList(APIView):
    permission_classes = (AllowAny,)

    def get(self, request, format=None):
        question_set = SensorReading.objects.all()
        serialized = SensorReadingSerializer(question_set, many=True)
        return Response(serialized.data)

    def post(self, request, format=None):
        serializer = SensorReadingSerializer(data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

class SensorReadingDetail(APIView):
    permission_classes = (AllowAny,)

    def get_object(self, pk):
        try:
            return SensorReading.objects.get(pk=pk)
        except SensorReading.DoesNotExist:
            raise Http404

    def get(self, request, pk, format=None):
        reading = self.get_object(pk)
        serialized = SensorReadingSerializer(reading)
        return Response(serialized.data)
