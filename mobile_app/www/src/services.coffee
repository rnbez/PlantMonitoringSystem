angular.module('starter.services', [])
.service "UserService", ($http) ->
  LOCAL_TOKEN_KEY = "TEST_LOCAL_KEY"
  currentUser =
    isAuthenticated = false
  endpoints =
    #host: 'http://ec2-52-10-29-10.us-west-2.compute.amazonaws.com'
    host: 'http://localhost:85'
    authenticate: () -> "#{endpoints.host}/api/user/authenticate"

  #API
  @authenticate = (username, pass) ->
    user =
      username: username
      pass: pass
    req = $http.post(endpoints.authenticate(), user)
    req.then (result) ->
      authUser = result.data
      window.localStorage.setItem(LOCAL_TOKEN_KEY, authUser.token)
      $http.defaults.headers.common['X-Auth-Token'] = authUser.token
      currentUser.isAuthenticated = true
      result.data

  @logout = () ->
    currentUser.isAuthenticated = false
    $http.defaults.headers.common['X-Auth-Token'] = undefined
    window.localStorage.removeItem(LOCAL_TOKEN_KEY)

  @isCurrentUserAuthenticated = () ->
    console.log window.localStorage.getItem(LOCAL_TOKEN_KEY)
    return true

  @getUser = (userId) ->
    req = $http.get("http://localhost:85/api/user/1")
    req.then (result) ->
      result.data
  return this


.service "NodeService", ($http) ->
  endpoints =
    #host: 'http://ec2-52-10-29-10.us-west-2.compute.amazonaws.com'
    host: 'http://localhost:85'
    getNodes: () -> "#{endpoints.host}/api/view/nodes"
    getNodeDetails: (params) -> "#{endpoints.host}/api/node/#{params.nodeId}"
    toggleLight: (id, status) -> "#{endpoints.host}/api/node/#{id}/light/#{status}"
    toggleWater: (id, status) -> "#{endpoints.host}/api/node/#{id}/water/#{status}"
  #API
  @getNodes = () ->
    req = $http.get(endpoints.getNodes())
    req.then (result) ->
      result.data

  @getNodeDetails = (params) ->
    req = $http.get(endpoints.getNodeDetails(params))
    req.then (result) ->
      result.data

  @toggleLight = (id, status) ->
    req = $http.post(endpoints.toggleLight(id, status))
    req.then (result) ->
      result.data

  @toggleWater = (id, status) ->
    req = $http.post(endpoints.toggleWater(id, status))
    req.then (result) ->
      result.data

  return this

.service "SensorService", ($http) ->
  endpoints =
    #host: 'http://ec2-52-10-29-10.us-west-2.compute.amazonaws.com'
    host: 'http://localhost:85'
    getSensor: (params) -> "#{endpoints.host}/api/sensor/#{params.sensorId}"
    getReadings: (params) -> "#{endpoints.host}/api/view/sensor/#{params.sensorId}/readings"

  #API
  @getSensor = (params) ->
    req = $http.get(endpoints.getSensor(params))
    req.then (result) ->
      result.data

  @getReadings = (params) ->
    req = $http.get(endpoints.getReadings(params))
    req.then (result) ->
      result.data

  return this

.factory "AuthInspector", ($rootScope, $q, AUTH_EVENTS) ->
  responseError: (response) ->
    $rootScope.$broadcast(
      {
      401: AUTH_EVENTS.notAuthenticated
      403: AUTH_EVENTS.notAuthorized
      }
      [response.status]
      response
    )
    return $q.reject(response)

.config ($httpProvider) ->
  $httpProvider.interceptors.push 'AuthInspector'
