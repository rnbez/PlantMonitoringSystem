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
        online: true
        sensors: [
          {
            id: 14
            friendlyName: "Air Temperature"
            lastReading: 24
            measurementUnit: "°C"
          }
          {
            id: 15
            friendlyName: "Air Humidity Sensor"
            lastReading: 38
            measurementUnit: "%"
          }
          {
            id: 16
            friendlyName: "Luminosity Sensor"
            lastReading: 45
            measurementUnit: "%"
          }
          {
            id: 17
            friendlyName: "Soil Temperature Sensor"
            lastReading: 18
            measurementUnit: "°C"
          }]
      }
      {
        id: 5
        physicalAddress: "E4-F8-9C-18-BA-B0"
        friendlyName: "Arduino Uno"
        behaviorId: 1
        online: false
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
          "14:26": Math.floor (Math.random() * 40) + 10
          "14:28": Math.floor (Math.random() * 40) + 10
          "14:29": Math.floor (Math.random() * 40) + 10
          "14:33": Math.floor (Math.random() * 40) + 10
          "14:34": Math.floor (Math.random() * 40) + 10
          "14:35": Math.floor (Math.random() * 40) + 10
      }
      {
        period: "Last Hours"
        values:
          "09h": Math.floor (Math.random() * 40) + 18
          "10h": Math.floor (Math.random() * 40) + 18
          "11h": Math.floor (Math.random() * 40) + 18
          "12h": Math.floor (Math.random() * 40) + 18
          "13h": Math.floor (Math.random() * 40) + 18
          "14h": Math.floor (Math.random() * 40) + 18
      }
      {
        period: "Last Days"
        values:
          "11/08": Math.floor (Math.random() * 40) + 25
          "11/09": Math.floor (Math.random() * 40) + 25
          "11/10": Math.floor (Math.random() * 40) + 25
          "11/12": Math.floor (Math.random() * 40) + 25
          "11/13": Math.floor (Math.random() * 40) + 25
          "11/14": Math.floor (Math.random() * 40) + 25
      }
    ]

  return this
