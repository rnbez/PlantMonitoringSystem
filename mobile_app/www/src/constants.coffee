angular.module('starter.constants', [])
.constant 'API',
  host: 'http://ec2-52-10-239-20.us-west-2.compute.amazonaws.com'
  #host: 'http://localhost:85'
.constant 'EVENTS',
  auth:
    authenticated: 'auth-authenticated'
    notAuthenticated: 'auth-not-authenticated'
    notAuthorized:  'auth-not-authorized'
  login:
    close: 'close-login-modal'
