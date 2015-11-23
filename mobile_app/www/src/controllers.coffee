angular.module('starter.controllers', [])
.controller 'AppCtrl', ($scope, $ionicModal, $timeout, UserService) ->
  console.log UserService.isCurrentUserAuthenticated()
  $scope.isAuthenticated = UserService.isCurrentUserAuthenticated()

  $scope.doLogout = ->
    UserService.logout()


  return

.controller 'LoginController', ($scope, UserService) ->
  $scope.user = {}
  $scope.loginError = false
  $scope.doLogin = () ->
    req = UserService.authenticate $scope.user.username, $scope.user.password
    req.then ((response) ->
      #success
      console.log response
    ), ((response) ->
      #error
      console.log "not logged"
    )


.controller 'NodesController', ($scope, nodes, NodeService) ->

  #console.log "I'm inside NodesController"
  console.log nodes
  $scope.nodes = nodes
  $scope.onlyOnlineNodes = true

  $scope.onRefresh = () ->
    query = NodeService.getNodes()
    query.then (response) ->
      $scope.nodes = response
      console.log $scope.nodes
      _.delay (->
        $scope.$broadcast('scroll.refreshComplete')
        return
      ), 1000


.controller 'SensorController', ($scope, $stateParams, sensor, readings, SensorService) ->

  $scope.options = {
    animation: false
  }
  $scope.sensor = sensor
  $scope.readings = _.map readings, (r) ->
    return {
      name: r.name
      labels: _.keys(r.values)
      data: [ _.values(r.values) ]
    }

  $scope.series = [sensor.measurementName]

  $scope.onClick = (points, evt) ->
    console.log points, evt


  $scope.onRefresh = () ->
    query = SensorService.getReadings($stateParams)
    query.then (response) ->
      $scope.readings = _.map response, (r) ->
        return {
          name: r.name
          labels: _.keys(r.values)
          data: [ _.values(r.values) ]
        }
      console.log $scope.readings
      _.delay (->
        $scope.$broadcast('scroll.refreshComplete')
        return
      ), 1000




  return


.controller 'NodeDetailsController', ($scope, node, NodeService) ->

  console.log node
  $scope.node = node

  $scope.toggleLight = ->
    console.log "toggle"
    query = NodeService.toggleLight(node.id, node.lightOn)
    query.then (response) ->
      #$scope.node = response
      console.log response

  $scope.toggleWater = ->
    console.log "toggle"
    query = NodeService.toggleWater(node.id, node.waterOn)
    query.then (response) ->
      #$scope.node = response
      console.log response
