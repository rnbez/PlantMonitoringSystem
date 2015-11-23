angular.module('starter.services', [])
.service "UserService", ($http, $rootScope, EVENTS, API) ->
  LOCAL_TOKEN_KEY = "TEST_LOCAL_KEY"
  currentUser =
    isAuthenticated = false
  endpoints =
    host: API.host
    authenticate: () -> "#{endpoints.host}/api/user/authenticate"
    removeAuthenticatedUser: () -> "#{endpoints.host}/api/user/authenticate/remove"
    create: () -> "#{endpoints.host}/api/user/create"

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
      $rootScope.$broadcast(EVENTS.auth.authenticated);
      result.data

  @logout = () ->
    user =
      token: window.localStorage.getItem(LOCAL_TOKEN_KEY)
    console.log user
    req = $http.post(endpoints.removeAuthenticatedUser(), user)
    req.then ((result) ->
      console.log "token removed from server"
      currentUser.isAuthenticated = false
      $http.defaults.headers.common['X-Auth-Token'] = undefined
      window.localStorage.removeItem(LOCAL_TOKEN_KEY)
    ), ((response) ->
      currentUser.isAuthenticated = false
      $http.defaults.headers.common['X-Auth-Token'] = undefined
      window.localStorage.removeItem(LOCAL_TOKEN_KEY)
    )

  @create = (username, pass, email) ->
    user =
      username: username
      pass: pass
      email: email
    req = $http.post(endpoints.create(), user)
    req.then (result) ->
      authUser = result.data
      window.localStorage.setItem(LOCAL_TOKEN_KEY, authUser.token)
      $http.defaults.headers.common['X-Auth-Token'] = authUser.token
      currentUser.isAuthenticated = true
      $rootScope.$broadcast(EVENTS.auth.authenticated);
      result.data

  @isCurrentUserAuthenticated = () ->
    console.log window.localStorage.getItem(LOCAL_TOKEN_KEY)
    return window.localStorage.getItem(LOCAL_TOKEN_KEY) != null

  @getUser = (userId) ->
    req = $http.get("http://localhost:85/api/user/1")
    req.then (result) ->
      result.data
  return this


.service "NodeService", ($http, API) ->
  endpoints =
    host: API.host
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

.service "SensorService", ($http, API) ->
  endpoints =
    host: API.host
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

.factory "AuthInspector", ($rootScope, $q, EVENTS) ->
  responseError: (response) ->
    if response.status == 403
      $rootScope.$broadcast(EVENTS.auth.notAuthenticated)

  # $rootScope.$broadcast(
  #   {
  #     401: EVENTS.auth.notAuthenticated
  #     403: EVENTS.auth.notAuthorized
  #   }
  #   [response.status]
  #   response
  # )
    return $q.reject(response)

.config ($httpProvider) ->
  $httpProvider.interceptors.push 'AuthInspector'
