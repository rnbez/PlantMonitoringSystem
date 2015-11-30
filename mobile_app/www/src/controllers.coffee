angular.module('starter.controllers', [])
.controller 'AppCtrl', ($scope, $state, $window, $ionicModal, $ionicPopup, UserService, EVENTS) ->
  $scope.loginData = {}
  $scope.isFormLoading = false
  $scope.isAuthenticated = UserService.isCurrentUserAuthenticated()

  $ionicModal.fromTemplateUrl('templates/signin.html', scope: $scope)
  .then (modal) ->
    $scope.signInModal = modal
    $scope.openLoginIfNotAuth()
    return

  $ionicModal.fromTemplateUrl('templates/signup.html', scope: $scope)
  .then (modal) ->
    $scope.signUpModal = modal
    return

  $scope.signin = ->
    $scope.signInModal.show()

  $scope.closeSignin = ->
    $scope.signInModal.hide()
    $scope.isAuthenticated = UserService.isCurrentUserAuthenticated()

  $scope.signup = ->
    $scope.signUpModal.show()

  $scope.closeSignup = ->
    $scope.signUpModal.hide()

  $scope.doLogin = () ->
    $scope.isFormLoading = true
    _.delay (->
      req = UserService.authenticate $scope.loginData.username, $scope.loginData.password
      req.then ((response) ->
        #success
        console.log response
        $scope.loginData = {}
        $scope.isFormLoading = false
        $scope.closeSignin()
      ), ((response) ->
        #error
        $scope.isFormLoading = false
        $scope.showPopup 'Login failed', 'Incorrect Username or Password.'
      )
      return
    ), 1000

  $scope.doSignUp = () ->
    $scope.isFormLoading = true
    _.delay (->
      req = UserService.create $scope.loginData.username, $scope.loginData.password, $scope.loginData.email
      req.then ((response) ->
        #success
        console.log response
        $scope.loginData = {}
        $scope.isFormLoading = false
        $scope.closeSignin()
        $scope.closeSignup()
      ), ((response) ->
        #error
        console.log response
        $scope.isFormLoading = false
        $scope.showPopup 'Register failed', response.data.message
      )
      return
    ), 1000


  $scope.doLogout = ->
    UserService.logout()
    #$state.go 'app.start', {}, reload: true
    $scope.signin()

  $scope.showPopup = (title, message) ->
    $ionicPopup.alert(
      title: title
      template: message
      buttons: [
        { text: 'Close', type: 'button-assertive' }
      ]
    )

  $scope.openLoginIfNotAuth = ->
    if !$scope.isAuthenticated
      $scope.signin()

  $scope.$on EVENTS.login.close, (event) ->
    console.log "close login modal"
    $scope.closeSignin()

  $scope.$on EVENTS.auth.notAuthenticated, (event) ->
    $scope.signin()


  return

.controller 'NodesController', ($scope, $stateParams, $ionicLoading, NodeService, EVENTS) ->

  console.log "I'm inside NodesController"
  $scope.nodes = []
  $scope.onlyOnlineNodes = true
  $scope.isLoading = false

  $scope.onRefresh = () ->
    req = NodeService.getNodes()
    req.then (response) ->
      $scope.nodes = response
      console.log $scope.nodes
      _.delay (->
        $scope.$broadcast('scroll.refreshComplete')
        return
      ), 1000

  $scope.loadNodes = () ->
    $ionicLoading.show(
      template: '<ion-spinner icon="android"></ion-spinner>'
      #noBackdrop: false
    )
    req = NodeService.getNodes()
    req.then ((response) ->
      $scope.nodes = response
      console.log $scope.nodes
      #$scope.$emit(EVENTS.login.close)
      $ionicLoading.hide()
    ), ((response) ->
      $ionicLoading.hide()
    )
  $scope.loadNodes()

  $scope.$on EVENTS.auth.authenticated, (event) ->
    $scope.loadNodes()



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
