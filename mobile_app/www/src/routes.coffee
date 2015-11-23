angular.module('starter.routes', [])
.config ($stateProvider, $urlRouterProvider) ->
  $stateProvider
    .state('app',
      url: '/app'
      abstract: true
      templateUrl: 'templates/menu.html'
      controller: 'AppCtrl')

    .state 'app.error',
      url: '/error'
      views: 'menuContent':
        templateUrl: 'templates/error.html'

    .state 'app.search',
      url: '/search'
      views: 'menuContent':
        templateUrl: 'templates/search.html'

    .state 'app.browse',
      url: '/browse'
      views: 'menuContent':
        templateUrl: 'templates/browse.html'

    .state 'app.login',
      url: '/login'
      views: 'menuContent':
        templateUrl: 'templates/login.html'
        controller: 'LoginController'

    .state 'app.nodes',
      url: '/nodes'
      views: 'menuContent':
        templateUrl: 'templates/nodes.html'
        controller: 'NodesController'
        resolve:
          nodes: (NodeService) ->
            NodeService.getNodes()

    .state 'app.node_sensor',
      url: '/sensor/:sensorId'
      views: 'menuContent':
        templateUrl: 'templates/sensor.html'
        controller: 'SensorController'
        resolve:
          sensor: (SensorService, $stateParams) ->
            SensorService.getSensor($stateParams)
          readings: (SensorService, $stateParams) ->
            SensorService.getReadings($stateParams)

    .state 'app.node_details',
      url: '/node/:nodeId'
      views: 'menuContent':
        templateUrl: 'templates/node_details.html'
        controller: 'NodeDetailsController'
        resolve:
          node: (NodeService, $stateParams) ->
            NodeService.getNodeDetails($stateParams)

    .state 'app.node_edit',
      url: '/node/:nodeId/edit'
      views: 'menuContent':
        templateUrl: 'templates/node_edit.html'
        controller: 'NodeDetailsController'
        resolve:
          node: (NodeService, $stateParams) ->
            NodeService.getNodeDetails($stateParams)

    .state 'app.node_behavior',
      url: '/node/:nodeId/behavior'
      views: 'menuContent':
        templateUrl: 'templates/node_behavior.html'
        controller: 'NodeDetailsController'
        resolve:
          node: (NodeService, $stateParams) ->
            NodeService.getNodeDetails($stateParams)




  $urlRouterProvider.otherwise '/app/nodes'
  return
