import sys, os

physical_addess = ''

def getMacAddress():
    if sys.platform == 'win32':
        for line in os.popen("ipconfig /all"):
            if line.lstrip().startswith('Physical Address'):
                mac = line.split(':')[1].strip().replace('-',':')
                break
            else:
                for line in os.popen("/sbin/ifconfig"):
                    if line.find('Ether') > -1:
                        mac = line.split()[4]
                        break
    return mac


def get():
    global physical_addess
    if not physical_addess:
        physical_addess = getMacAddress()
    return {
                "id": 1,
                "physicalAddress": physical_addess,
                "friendlyName": "Raspberry PI 2",
                "behaviorId": 1,
                "sensors": [
                    {
                        "id": 4,
                        "sensorName": "ds18b20",
                        "friendlyName": "Soil Temperature Sensor",
                        "measurementName": "temperature",
                        "measurementUnit": "celsius",
                        "node": 1
                    },
                    {
                        "id": 5,
                        "sensorName": "moisture_sensor",
                        "friendlyName": "Moisture Sensor",
                        "measurementName": "moisture",
                        "measurementUnit": "%",
                        "node": 1
                    },
                    {
                        "id": 3,
                        "sensorName": "ldr",
                        "friendlyName": "Luminosity Sensor",
                        "measurementName": "luminosity",
                        "measurementUnit": "%",
                        "node": 1
                    },
                    {
                        "id": 1,
                        "sensorName": "dht_11_temp",
                        "friendlyName": "Air Temperature",
                        "measurementName": "temperature",
                        "measurementUnit": "celsius",
                        "node": 1
                    },
                    {
                        "id": 2,
                        "sensorName": "dht_11_humidity",
                        "friendlyName": "Air Humidity Sensor",
                        "measurementName": "humidity",
                        "measurementUnit": "%",
                        "node": 1
                    }
                ]
            }

def update():
    return 1
