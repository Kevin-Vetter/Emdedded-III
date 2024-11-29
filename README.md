# IOT2 & Webapi og App
<p style="font-size:23px;">mkrwifi1010 summary </p>
This project is an exercise for how to connect an mkrwifi1010
to mqtt broker and send messages to a topic, subscribe & publish.

<img src="Board.jpg" width="400" height="400" />

---
<p style="font-size:23px;">
WebApi sumarry</p>

someting

<img src="swaggerhub-swaggerui.png" width="800" height="800" />


## T.O.C
- [Table of content](#toc)
- [Mkrwifi1010](#mkrwifi1010)
    - [Installation](#installation-mkrwifi10)
    - [Usage](#usage-mkrwifi1010)
    - [Library](#library-mkrwifi1010y)
    - [Configurations](#configurations-mkrwifi1010s)
        - [Broker](#broker-mkrwifi1010)
        - [Topic](#topic-mkrwifi1010)
        - [Wifi](#wifi-mkrwifi1010)
- [WebApi](#webapi)
    - [Installation](#installation-webapi)
    - [Usage](#usage-webapi)
    - [Library](#library-webapi)
- [Contributing](#Contributing)

---

## Mkrwifi1010
this arduino mkrwifi1010

it sucks

but i did this shit anyway 


blah

blah

## Installation Mkrwifi10
configure the arduino board


## Usage Mkrwifi1010

lorem ipsum
WIP




## Library MKRWIFI1010 

|  Name  | version  | link |
|---|:---:|---:|
| Adafruit Unified Sensor | 1.1.14 | [Link](https://github.com/adafruit/Adafruit_Sensor)|
| ArduinoBearSSL   | 1.7.4 | [Link](https://github.com/arduino-libraries/ArduinoBearSSL)|
| DHT sensor library | 1.4.6 | [Link](https://github.com/adafruit/DHT-sensor-library) |
| Servo | 1.2.2 | [Link](https://www.arduino.cc/reference/en/libraries/servo/) |
| WiFiNINA | 1.8.14 | [Link](http://www.arduino.cc/en/Reference/WiFiNINA) |



## Configurations Mkrwifi1010

WIP



### Broker Mkrwifi1010
These controls what broker you should connect to
```c++   
    const char broker[] = "Change-Me.to.a.broker.com";      
    int port = 8883;
```
Inside BrokerSecret.h change these to your username and pass
```c++   
#define SECRETBroker_User ""
#define SECRETBroker_PASS ""
```
----

#### Topic Mkrwifi1010
This value controls what topic you are subscribed to
```c++
    const char topic[]= "arduino/simple";
``` 
 
#### Wifi Mkrwifi1010
Inside WifiSecret.h change the user and pass for wifi
```c++
char ssid[] = SECRET_SSID;    // your network SSID (name)
char pass[] = SECRET_PASS;    // your network password (use for WPA, or use as key for WEP)
```



___
<br/>


## Webapi

### Installation WebApi
To install this project, run Webapi.exe

### Library WebApi
|  Name  | version  | link |
|---|:---:|---:|
| Lorem | 1.1.1 | [Link](https://github.com/adafruit/Adafruit_Sensor) |
| Lorem | 1.7.4 | [Link](https://github.com/arduino-libraries/ArduinoBearSSL) |
| Lorem | 1.4.6 | [Link](https://github.com/adafruit/DHT-sensor-library) |
| Lorem | 1.2.2 | [Link](https://www.arduino.cc/reference/en/libraries/servo/) |
| Lorem | 1.8.1 | [Link](http://www.arduino.cc/en/Reference/WiFiNINA) |

### Usage WebApi
you post and get request form your ip or url

http://ur.api.url.com/api or http://127.0.0.1/api



## App

### Installation App

### Libary Maui
$Mauiversion = 8.0.0;

|  Name  | version  | link |
|---|:---:|---:|
| CommunityToolkit.Mvvm | 8.4.0 | [Link](https://github.com/adafruit/Adafruit_Sensor) |
| Lorem | 8.0.0 | [Link](https://github.com/arduino-libraries/ArduinoBearSSL) |
| Lorem | $Mauiversion | [Link](https://github.com/adafruit/DHT-sensor-library) |
| Lorem | 1.2.2 | [Link](https://www.arduino.cc/reference/en/libraries/servo/) |
| Lorem | 1.8.1 | [Link](http://www.arduino.cc/en/Reference/WiFiNINA) |




## Contributing

- Elias
- Kevin
- Max



