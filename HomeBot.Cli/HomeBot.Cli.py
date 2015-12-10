import sys, serial, time
import urllib, urllib2
import json

# configure the serial connections (the parameters differs on the device you are connecting to)
ser = serial.Serial(
    port='/dev/ttyUSB0',
    baudrate=9600,
    parity=serial.PARITY_ODD,
    stopbits=serial.STOPBITS_TWO,
    bytesize=serial.SEVENBITS
)

apiUrl = "https://api.thingspeak.com/update"
apiKey = "CHNLDXPXT2S5L19L"

def init():
    ser.isOpen()
    print ('Started')

def sendMotion666(data):
    print ("sendToServer: " + str(data))
    url = 'http://www.google.com/'
    params = urllib.urlencode({
      'api_key': apiKey,
      'field2': data
    })
    try:
        response = urllib2.urlopen(apiUrl, params).read()
    except urllib2.HTTPError as err:
        print("Exception: {0}".format(err))
    except:
        print("Enhadled error:", sys.exc_info()[0])
    pass

def decodeMessage(jsonStr):
    #print ("decoding json: " + jsonStr)
    sparams = '';
    try:
        obj = json.loads(jsonStr)
        if(len(obj["rfcodes"]) > 0):        
            for code in obj["rfcodes"]:
                #sparams  += str(code) + ','
                sendMotion666(code)
    except TypeError as err:
        print("TypeError: {0}".format(err))
    except ValueError as err:
        print("ValueError: {0}".format(err))
        
    except:
        print("decode error:", sys.exc_info()[0])
    pass
    

def fixJson(str):
    str = str.replace('msgid', '"msgid"')
    str = str.replace('rfcodes', '"rfcodes"')
    return str;

def mainLoop():
    while 1 :
        out = ''
        #time.sleep(1)
        while ser.inWaiting() > 0:
            out += ser.read(1)

        if out != '':
           #print (">>" + out)
           if out.find('{msgid') >= 0:
                out = fixJson(out)
                decodeMessage(out)


def main():
    obj = json.loads('{"msgid": 7, "rfcodes":[666]}')
    print(obj['msgid'])
    print(obj['rfcodes'])
    print(len(obj['rfcodes']))
    
    init()
    mainLoop()

if __name__ == "__main__":
    sys.exit(int(main() or 0))
