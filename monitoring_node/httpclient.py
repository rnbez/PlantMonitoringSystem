import json, httplib, urllib, api

def get(path):
    headers = {"Content-type": "application/json", "Accept": "application/json"}
    conn = httplib.HTTPConnection(api.__host__, api.__port__)
    conn.request("GET", path)
    response = conn.getresponse()
    response.body = response.read()
    conn.close()
    return response

def post(path, data):
    reading = str(data)
    body = json.dumps(data)
    headers = {"Content-type": "application/json", "Accept": "application/json"}
    #print "\n##DEBUG##\n"
    #print host, ":", str(port), path
    #print body
    conn = httplib.HTTPConnection(api.__host__, api.__port__, timeout=2)
    conn.request("POST", path, body, headers)
    response = conn.getresponse()
    #print response.status, response.reason
    response.body = response.read()
    conn.close()
    return response
# serialize -> json.dumps(f.__dict__)
# deserialize -> json.loads('')
