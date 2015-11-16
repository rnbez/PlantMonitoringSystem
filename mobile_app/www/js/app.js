// Generated by CoffeeScript 1.10.0
var dependencies;

dependencies = ['ionic', 'chart.js', 'starter.controllers', 'starter.services', 'starter.routes'];

angular.module('starter', dependencies).run(function($ionicPlatform) {
  $ionicPlatform.ready(function() {
    if (window.cordova && window.cordova.plugins.Keyboard) {
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
      cordova.plugins.Keyboard.disableScroll(true);
    }
    if (window.StatusBar) {
      StatusBar.styleDefault();
    }
  });
});
