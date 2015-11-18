dependencies = [
  'ionic'
  'chart.js'
  'starter.controllers'
  'starter.services'
  'starter.routes'
]

angular.module('starter', dependencies)
.run ($ionicPlatform) ->
  $ionicPlatform.ready ->
    if window.cordova and window.cordova.plugins.Keyboard
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar true
      cordova.plugins.Keyboard.disableScroll true
    if window.StatusBar
      StatusBar.styleDefault()
    return
  return
