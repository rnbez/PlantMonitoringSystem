# Plant Monitoring System

The Plant Monitoring System consists of an web application that continuously receive data from a monitoring node. The web app should also display the data received and secure the access to the information. All the automated tasks should have a manual mode, in which the user himself start the task.


<div style="text-align:center"><img src ="https://github.com/rafaelbezerra-dev/PlantMonitoringSystem/blob/master/doc/project.min.jpg" /></div>

The system is separated 4 parts: the **Server**, the **Database**, the **Mobile App** and the **Monitoring Node**.

## IoT level
<div style="text-align:center"><img src ="https://github.com/rafaelbezerra-dev/PlantMonitoringSystem/blob/master/doc/iot_level.min.jpg" /></div>

## Hardware: 

The system is physically separated in 4 parts. These parts are:
* 
Server ― AWS EC2 Instance (t2-micro) running Windows Server 2012
* 
Database ― AWS RDS Instance (db.t2.micro) running MySQL 5.6.23
* 
Mobile App ― Any Android 4.X.X Smartphone
* 
Monitoring Node ― Raspberry Pi 2 running Raspbian

The components connected to the monitoring node are:
* 
Adafruit MCP3008 - 8-Channel 10-Bit ADC With SPI Interface
* 
DIYmall DHT11 Temperature Relative Humidity Sensor Module
* 
Waterproof Probe with DS18B20 Temperature Sensor
* 
Arrela® Soil Hygrometer Detection Module (Soil Moisture Sensor)
* 
Light Dependent Resistor (LDR)
* 
Water Pump
* 
5W Lamp
* 
2-Channel Relay Module

Besides this components, a GPIO Extension Board was used to connect the Raspberry Pi to an 800-points breadboard and additional resistors and jumpers were used to wire the components together.

### Input and Output
* 
GPIO (for the DHT11 sensor and Relay Module control)
* 
I2C (for the DS18B20 sensor)
* 
SPI (for the ADC MCP3008)

### Network
The Monitoring Node uses a WIFI dongle to post data to the server through WAN, but it can work through LAN if an Ethernet cable is connected to it. The project also has a mobile app, which make it possible for the user to also use his mobile network to access information from the server.

## Software

### Communication model
The communication is based on requests and responses using REST architecture. The web server acts most of the time as a proxy for the database, making sure that only authenticated users can access the system information. There is no direct access to the database. Instead, every app (mobile, web or local) must perform authenticated requests to the server.

<div style="text-align:center"><img src ="https://github.com/rafaelbezerra-dev/PlantMonitoringSystem/blob/master/doc/server_communication.png" width="500" /></div>


An authenticated request is a HTTP Request containing a X-Auth-Token header with a valid authentication token, a GUID given by the server after the authentication URI is called if the parameters (username and password) are correct. The following image shows how the Monitoring Node acts as it is initialized, asking for credentials and storing the authentication token for future requests.

<div style="text-align:center"><img src ="https://github.com/rafaelbezerra-dev/PlantMonitoringSystem/blob/master/doc/activity_diagram.jpg" width="600"/></div>


### Data and Storage
The Monitoring Node code supports readings the measurements: Humidity, Light Level, Soil Moisture and Temperature (of the air and soil). All the readings are stored in a RDS machine on Amazon Web Service. The machine runs MySQL and has 20Gb of SSD storage.

### Analysis
The system is capable of generating an overview of the reading for some periods of time. When using the mobile app, the user can see 3 charts. The first contains average readings from the last hour. The second shows the last 24 hours and the third shows the last 7 days.


<div style="text-align:center">
<img src="https://github.com/rafaelbezerra-dev/PlantMonitoringSystem/blob/master/doc/charts_screenshot_2.png" width="300" />
<img src="https://github.com/rafaelbezerra-dev/PlantMonitoringSystem/blob/master/doc/charts_screenshot_1.png" width="300" />
</div>


## Links and Credits 

### Development
* 
[AngularJS](https://angularjs.org/)
* 
[CoffeeScript](http://coffeescript.org/)
* 
[Less](http://lesscss.org/)
* 
[Ionic Framework](http://ionicframework.com/)
* 
[Apache Cordova](https://cordova.apache.org/)
* 
[Python 2.7](https://www.python.org/download/releases/2.7/)
* 
[C# and .NET Framework](https://msdn.microsoft.com/pt-br/library/z1zx9t92.aspx)

### Documentation
* 
[Astah Community](http://astah.net/editions/community)
* 
[Gitbook](https://www.gitbook.com/)

### The Noum Project Icons
* 
[Raspberry Pi B+](https://thenounproject.com/fredley/collection/raspberry-pi/?oq=raspberry&cidx=0&i=139697) by Tom Medley from the Noun Project
* 
[Thermometer](https://thenounproject.com/search/?q=temperature&i=100988) by Evan Shuster from the Noun Project
* 
[Humidity](https://thenounproject.com/search/?q=Humidity&i=102422) by Evgeniy Artsebasov from the Noun Project
* 
[WiFi](https://thenounproject.com/search/?q=signal&i=117221) by Yamini Ahluwalia from the Noun Project
* 
[Cloud Secure](https://thenounproject.com/imicons/collection/cloud/?oq=cloud&cidx=0&i=68230) by iconsmind.com from the Noun Project
* 
[Photo Resistor](https://thenounproject.com/search/?q=resistor&i=120881) by Arthur Shlain from the Noun Project
* 
[Light](https://thenounproject.com/search/?q=led+light&i=115381) by Creative Stall from the Noun Project
* 
[Pipe valve](https://thenounproject.com/search/?q=water+pipe&i=175810) by Creative Stall from the Noun Project
* 
[Valve](https://thenounproject.com/search/?q=water+supply&i=119129) by Sergey Demushkin from the Noun Project
* 
[Water Tank](https://thenounproject.com/search/?q=water+reservoir&i=173863) by Adam Zubin from the Noun Project
* 
[Statistics](https://thenounproject.com/search/?q=statistics&i=155834) by Hermine Blanquart from the Noun Project
* [Equalizer](https://thenounproject.com/search/?q=equalizer&i=146796) by Dominique Vicent from the Noun Project
* 
[Smartphone](https://thenounproject.com/search/?q=cell+phone&i=32550) by Martin Jordan from the Noun Project
* 
[Laptop](https://thenounproject.com/search/?q=pc&i=25782) by Pham Thi Dieu Linh from the Noun Project
* 
[Sprout](https://thenounproject.com/search/?q=plant&i=145902) by Agne Alesiute from the Noun Project


