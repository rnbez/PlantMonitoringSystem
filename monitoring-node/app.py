from sensors import *
import time, datetime, json, httplib, urllib

def __get_formtated_datetime():
    return datetime.datetime.now().isoformat()

def __post_to_server(data):
    path = "/api/monitor/reading/"
    print "\n##DEBUG##\n"
    print json.dumps(data.__dict__)
    body = json.dumps(data.__dict__)
    headers = {"Content-type": "application/json", "Accept": "application/json"}
    conn = httplib.HTTPConnection("http://127.0.0.1:8000")
    conn.request("POST", path, body, headers)
    response = conn.getresponse()
    print response.status, response.reason

    data = response.read()
    data
    'Redirecting to <a href="http://bugs.python.org/issue12524">http://bugs.python.org/issue12524</a>'
    conn.close()
# serialize -> json.dumps(f.__dict__)
# deserialize -> json.loads('')

while True:
    
    #__post_to_server()

    try:
        print "Soil: Temp.: {} & Moisture:{}".format(str(SensorReader.read_air_temperature()), str(SensorReader.read_moisture()))
        print "Env: Temp.: {} & Luminosity:{}".format(str(SensorReader.read_soil_temperature()), str(SensorReader.read_luminosity()))
        print "\n"


        time.sleep(10)
    except KeyboardInterrupt:
        exit()
