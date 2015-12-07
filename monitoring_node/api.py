__host__ = 'ec2-52-10-239-20.us-west-2.compute.amazonaws.com'
__port__ = 80

# ROUTES
__handshake__       = '/api/handshake/'
__send_readings__   = '/api/reading/'
__authenticate__   = '/api/user/authenticate'


# TOKEN
AUTH_TOKEN = ''

def __get_node__(nodeId):
    return '/api/node/' + str(nodeId) + '?includeSensors=true'

def __get_node_view__(nodeId):
    return '/api/view/node/' + str(nodeId)

def __toggle_light__(nodeId, status):
    return '/api/node/#{id}' + str(nodeId) + '/light/' + str(status).lower()

def __toggle_light__(nodeId, status):
    return '/api/node/#{id}' + str(nodeId) + '/water/' + str(status).lower()

def __get_lightwater__(nodeId):
    return '/api/node/' + str(nodeId) + '?light=true&water=true'

def __get_behavior__(behaviorId):
    return '/api/behavior/' + str(behaviorId)
