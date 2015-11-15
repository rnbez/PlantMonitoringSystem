angular.module('starter.services', [])
.service "NodeService", ($http) ->
  endpoints =
    host: 'http://ec2-52-33-255-233.us-west-2.compute.amazonaws.com'
    getNodes: () -> "#{endpoints.host}/api/view/nodes"

  #API
  @getNodes = () ->
    req = $http.get(endpoints.getNodes())
    req.then (result) ->
      result.data

  #  list = [{
  #      id: 4
  #      physicalAddress: "b8:27:eb:b4:17:4a"
  #      friendlyName: "Raspberry PI 2"
  #      online: true
  #      sensors: [
  #        {
  #          id: 14
  #          friendlyName: "Air Temperature"
  #          lastReading: 24
  #          measurementUnit: "°C"
  #        }
  #        {
  #          id: 15
  #          friendlyName: "Air Humidity Sensor"
  #          lastReading: 38
  #          measurementUnit: "%"
  #        }
  #        {
  #          id: 16
  #          friendlyName: "Luminosity Sensor"
  #          lastReading: 45
  #          measurementUnit: "%"
  #        }
  #        {
  #          id: 17
  #          friendlyName: "Soil Temperature Sensor"
  #          lastReading: 18
  #          measurementUnit: "°C"
  #        }]
  #    }
  #    {
  #      id: 5
  #      physicalAddress: "E4-F8-9C-18-BA-B0"
  #      friendlyName: "Arduino Uno"
  #      behaviorId: 1
  #      online: false
  #    }]

  return this


.service "SensorService", ($http) ->
  endpoints =
    host: 'http://ec2-52-33-255-233.us-west-2.compute.amazonaws.com'
    getSensor: (params) -> "#{endpoints.host}/api/sensor/#{params.sensorId}"
    getReadings: (params) -> "#{endpoints.host}/api/view/sensor/#{params.sensorId}/readings"




  #API
  @getSensor = (params) ->
    req = $http.get(endpoints.getSensor(params))
    req.then (result) ->
      result.data
  #  sensor =
  #    id: params.sensorId
  #    sensorType: "dht11_temperature"
  #    friendlyName: "Air Temperature"
  #    measurementName: "temperature"
  #    measurementUnit: "celsius"
  #    node: 4

  @getReadings = (params) ->
    req = $http.get(endpoints.getReadings(params))
    req.then (result) ->
      result.data
  #  readings = [
  #    {
  #      name: "Last Hour"
  #      values:
  #        "00:00": Math.floor (Math.random() * 40) + 10
  #        "00:10": Math.floor (Math.random() * 40) + 10
  #        "00:20": Math.floor (Math.random() * 40) + 10
  #        "00:30": Math.floor (Math.random() * 40) + 10
  #        "00:40": Math.floor (Math.random() * 40) + 10
  #        "00:50": Math.floor (Math.random() * 40) + 10
  #        "01:00": Math.floor (Math.random() * 40) + 10
  #    }
  #    {
  #      name: "Last 24 Hours"
  #      values:
  #        "Fri 07h": Math.floor (Math.random() * 40) + 18
  #        "Fri 11h": Math.floor (Math.random() * 40) + 18
  #        "Fri 15h": Math.floor (Math.random() * 40) + 18
  #        "Fri 19h": Math.floor (Math.random() * 40) + 18
  #        "Fri 23h": Math.floor (Math.random() * 40) + 18
  #        "Sat 03h": Math.floor (Math.random() * 40) + 18
  #        "Sat 04h": Math.floor (Math.random() * 40) + 18
  #    }
  #    {
  #      name: "Last 7 Days"
  #      values:
  #        "11/07": Math.floor (Math.random() * 40) + 25
  #        "11/08": Math.floor (Math.random() * 40) + 25
  #        "11/09": Math.floor (Math.random() * 40) + 25
  #        "11/10": Math.floor (Math.random() * 40) + 25
  #        "11/12": Math.floor (Math.random() * 40) + 25
  #        "11/13": Math.floor (Math.random() * 40) + 25
  #        "11/14": Math.floor (Math.random() * 40) + 25
  #    }
  #  ]

  return this
