__host__ = 'ec2-52-10-29-10.us-west-2.compute.amazonaws.com'
__port__ = 80

# ROUTES
__handshake__       = '/api/handshake/'
__send_readings__   = '/api/reading/'

def __get_node__(nodeId):
    return '/api/node/' + str(nodeId) + '?includeSensors=true'
