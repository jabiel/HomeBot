import sys, serial, json, time

class ArduinoSerial(object):
    """Comunicates with arduino threw serial port (usb cable)"""

    def __init__(self, portName):
        self._portName = portName
        self._idleTime = 20 # po ilu sek moze sie ponownie zminic stan 
        self._lastStartTime = 0.0
        self._ser = serial.Serial(
            port=self._portName,#'/dev/ttyUSB0',
            baudrate=9600,
            parity=serial.PARITY_ODD,
            stopbits=serial.STOPBITS_TWO,
            bytesize=serial.SEVENBITS
        )
        self._ser.isOpen()

    def readFromSerial(self):
        out = ''
        while self._ser.inWaiting() > 0:
            out += self._ser.read(1)

        if out != '':
            return self.decodeCsvMessage(out)
        return {}
            #if out.find('{msgid') >= 0:
            #    out = self._fixJson(out)
            #    self.decodeJsonMessage(out, data)
            #else:
            #    print("Unexpexted json data: " + out)
    """csv messgae: DATA,3,1100001 where  DATA is header, 3 is timespan 1100001 is message from sensor id 11 with value 1"""
    def decodeCsvMessage(self, csvStr):
        data = {}
        try:
            if csvStr.startswith("DATA"):
                lst = csvStr.split(",")
                if len(lst) > 2:
                    for i in range(2, len(lst)):
                        sensorId = long(lst[i]) / 100000
                        sensorVal = long(lst[i]) - (sensorId * 100000)
                        data["sensor" + str(sensorId)] = sensorVal
       
                        print('sensor: {0}'.format(data))
            else:
                print('>{0}'.format(csvStr))

            return data        
        except:
            print("decode error:", sys.exc_info()[0])
        pass


    def decodeJsonMessage(self, jsonStr, data):
        #print ("decoding json: " + jsonStr)
        try:
            obj = json.loads(jsonStr)
            if(len(obj["rfcodes"]) > 0):        
                for code in obj["rfcodes"]:
                    print('rfcode:{0}'.format(code))
                    if (str(code) == '666') & ((self._lastStartTime + self._idleTime) < time.time()):
                        data['field2'] = 1
                        self._lastStartTime = time.time()
                        #GPIO.output(buzzer_pin, True)
        except TypeError as err:
            print("TypeError: {0}".format(err))
        except ValueError as err:
            print("ValueError: {0} | {1}".format(err, jsonStr))
        except NameError as err:
            print("NameError: {0}".format(err))
        
        except:
            print("decode error:", sys.exc_info()[0])
        pass    

    def _fixJson(self, str):
        str = str.replace('msgid', '"msgid"')
        str = str.replace('rfcodes', '"rfcodes"')
        return str;