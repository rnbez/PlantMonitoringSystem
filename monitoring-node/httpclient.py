import json, httplib, urllib

#constants
host = "localhost"
port = 85

#paths
PATH_READING = "/api/reading/"

def post(path, data):
    reading = str(data)
    body = json.dumps(data)
    headers = {"Content-type": "application/json", "Accept": "application/json"}
    #print "\n##DEBUG##\n"
    #print host, ":", str(port), path
    #print body
    conn = httplib.HTTPConnection(host, port)
    conn.request("POST", path, body, headers)
    response = conn.getresponse()
    #print response.status, response.reason
    #print data
    #'Redirecting to <a href="http://bugs.python.org/issue12524">http://bugs.python.org/issue12524</a>'
    conn.close()
    return response
# serialize -> json.dumps(f.__dict__)
# deserialize -> json.loads('')
