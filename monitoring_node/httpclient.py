import json, httplib, urllib, api

def get(path):
    headers = {"Content-type": "application/json",
               "Accept": "application/json",
               "X-Auth-Token": api.AUTH_TOKEN}
    conn = httplib.HTTPConnection(api.__host__, api.__port__, timeout=2)
    conn.request("GET", path, headers=headers)
    response = conn.getresponse()
    response.body = response.read()
    conn.close()
    return response

def post(path, data):
    reading = str(data)
    body = json.dumps(data)
    headers = {"Content-type": "application/json",
               "Accept": "application/json",
               "X-Auth-Token": api.AUTH_TOKEN}
    #print "\n##DEBUG##\n"
    #print host, ":", str(port), path
    #print body
    conn = httplib.HTTPConnection(api.__host__, api.__port__, timeout=4)
    conn.request("POST", path, body=body, headers=headers)
    response = conn.getresponse()
    #print response.status, response.reason
    response.body = response.read()
    conn.close()
    return response
# serialize -> json.dumps(f.__dict__)
# deserialize -> json.loads('')
