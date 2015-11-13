angular.module('starter.services', [])
.service "NodeService", ($http) ->
  #endpoints =
  #  getNodes: () -> "/api/nodes/"
  #  getNode: (id) -> "/api/node/#{id}/?includeSensors=true"

  #API
  @getNodes = () ->
    list = [{
        id: 4
        physicalAddress: "b8:27:eb:b4:17:4a"
        friendlyName: "Raspberry PI 2"
        behaviorId: 1,
        sensors: [
          {
            id: 14
            sensorType: "dht11_temperature"
            friendlyName: "Air Temperature"
            measurementName: "temperature"
            lastReading: 24
            measurementUnit: "°C"
            node: 4
          }
          {
            id: 15
            sensorType: "dht11_humidity"
            friendlyName: "Air Humidity Sensor"
            measurementName: "humidity"
            lastReading: 38
            measurementUnit: "%"
            node: 4
          }
          {
            id: 16
            sensorType: "ldr"
            friendlyName: "Luminosity Sensor"
            measurementName: "luminosity"
            lastReading: 45
            measurementUnit: "%"
            node: 4
          }
          {
            id: 17
            sensorType: "ds18b20"
            friendlyName: "Soil Temperature Sensor"
            measurementName: "temperature"
            lastReading: 18
            measurementUnit: "°C"
            node: 4
          }]
      }
      {
        id: 5
        physicalAddress: "E4-F8-9C-18-BA-B0"
        friendlyName: "Arduino Uno"
        behaviorId: 1
      }]

  return this


.service "SensorService", ($http) ->

  #API
  @getSensor = (params) ->
    sensor =
      id: params.sensorId
      sensorType: "dht11_temperature"
      friendlyName: "Air Temperature"
      measurementName: "temperature"
      measurementUnit: "celsius"
      node: 4

  @getReadings = (params) ->
    readings = [
      {
        period: "Last Readings"
        values:
          "14:26": 25
          "14:28": 48
          "14:29": 12
          "14:33": 34
          "14:34": 80
          "14:35": 60
      }
      {
        period: "Last Hours"
        values:
          "09h": 30
          "10h": 28
          "11h": 30
          "12h": 34
          "13h": 32
          "14h": 38
      }
      {
        period: "Last Days"
        values:
          "11/08": 25
          "11/09": 23
          "11/10": 18
          "11/12": 20
          "11/13": 26
          "11/14": 21
      }
    ]

  return this
