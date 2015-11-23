angular.module('starter.constants', [])
.constant 'EVENTS',
  auth:
    authenticated: 'auth-authenticated'
    notAuthenticated: 'auth-not-authenticated'
    notAuthorized:  'auth-not-authorized'
  login:
    close: 'close-login-modal'
