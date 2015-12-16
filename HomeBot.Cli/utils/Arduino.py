import sys, serial, json, time

class ArduinoSerial(object):
    """Comunicates with arduino threw serial port (usb cable)"""

    def __init__(self, portName):
        self._portName = portName
        self._idleTime = 20 # po ilu sek moze sie ponownie wlaczyc 
        self._lastStartTime = 0.0
        self._ser = serial.Serial(
            port=self._portName,#'/dev/ttyUSB0',
            baudrate=9600,
            parity=serial.PARITY_ODD,
            stopbits=serial.STOPBITS_TWO,
            bytesize=serial.SEVENBITS
        )
        self._ser.isOpen()

    def readFromSerial(self, data):
        out = ''
        while self._ser.inWaiting() > 0:
            out += self._ser.read(1)

        if out != '':
            #print (">>" + out)
            if out.find('{msgid') >= 0:
                out = self._fixJson(out)
                self.decodeMessage(out, data)
            else:
                print("Unexpexted serial data: " + out)

    def decodeMessage(self, jsonStr, data):
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