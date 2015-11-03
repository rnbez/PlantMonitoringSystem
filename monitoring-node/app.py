from sensors import *
import time, datetime, json, httplib, urllib

def __get_formtated_datetime():
    return datetime.datetime.now().isoformat()

def __post_to_server(data):
    host = "localhost"
    port = 85
    path = "/api/readings/"
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

    data = response.read()
    #print data
    #'Redirecting to <a href="http://bugs.python.org/issue12524">http://bugs.python.org/issue12524</a>'
    conn.close()
# serialize -> json.dumps(f.__dict__)
# deserialize -> json.loads('')

while True:

    try:
        air_temp = SensorReader.read_air_temperature()
        __post_to_server({"reading":air_temp,"date": __get_formtated_datetime(),"sensor": 1})
        air_hum = SensorReader.read_air_humidity()
        __post_to_server({"reading":air_hum,"date": __get_formtated_datetime(),"sensor": 2})
        air_lum = SensorReader.read_luminosity()
        __post_to_server({"reading":air_lum,"date": __get_formtated_datetime(),"sensor": 3})
        print "Env: Temp.: {}C  Humidity: {}%  &  Luminosity:{}%".format(str(air_temp), str(air_hum), str(air_lum))

        soil_temp = SensorReader.read_soil_temperature()
        __post_to_server({"reading":soil_temp,"date": __get_formtated_datetime(),"sensor": 4})
        moisture = SensorReader.read_moisture()
        __post_to_server({"reading":moisture,"date": __get_formtated_datetime(),"sensor": 5})
        print "Soil: Temp.: {}C  &  Moisture:{}".format(str(soil_temp), str(moisture))
        print "\n"


        time.sleep(2)
    except KeyboardInterrupt:
        exit()
