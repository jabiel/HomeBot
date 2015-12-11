#!/usr/bin/env python
import sys, serial, time, json
import logging, logging.handlers
import RPi.GPIO as GPIO
import Thingspeak


LOG_FILENAME = "/var/log/homebot.log"
LOG_LEVEL = logging.INFO
logger = logging.getLogger(__name__)
logger.setLevel(LOG_LEVEL)
handler = logging.handlers.TimedRotatingFileHandler(LOG_FILENAME, when="midnight", backupCount=3)
formatter = logging.Formatter('%(asctime)s %(levelname)-8s %(message)s')
handler.setFormatter(formatter)
logger.addHandler(handler)
logger.info("Initializing script")

thingspeak = Thingspeak.ThingspeakClient('CHNLDXPXT2S5L19L')
thingspeak_delay = 15
buzzer_pin = 11

ser = serial.Serial(
    port='/dev/ttyUSB0',
    baudrate=9600,
    parity=serial.PARITY_ODD,
    stopbits=serial.STOPBITS_TWO,
    bytesize=serial.SEVENBITS
)

class MyLogger(object):
    def __init__(self, logger, level):
        """Needs a logger and a logger level."""
        self.logger = logger
        self.level = level

    def write(self, message):
        if message.rstrip() != "":
            self.logger.log(self.level, message.rstrip())

sys.stdout = MyLogger(logger, logging.INFO)
# Replace stderr with logging to file at ERROR level
sys.stderr = MyLogger(logger, logging.ERROR)

def init():
    

    ser.isOpen()
    GPIO.setmode(GPIO.BOARD)
    GPIO.setup(buzzer_pin, GPIO.OUT)
    GPIO.output(buzzer_pin, False)
    print ('Started')


def decodeMessage(jsonStr, data):
    #print ("decoding json: " + jsonStr)
    try:
        obj = json.loads(jsonStr)
        if(len(obj["rfcodes"]) > 0):        
            for code in obj["rfcodes"]:
                print('rfcode:{0}'.format(code))
                if str(code) == '666':
                    data['field2'] = 1
                    GPIO.output(buzzer_pin, True)
    except TypeError as err:
        print("TypeError: {0}".format(err))
    except ValueError as err:
        print("ValueError: {0} | {1}".format(err, jsonStr))
    except NameError as err:
        print("NameError: {0}".format(err))
        
    except:
        print("decode error:", sys.exc_info()[0])
    pass
    

def fixJson(str):
    str = str.replace('msgid', '"msgid"')
    str = str.replace('rfcodes', '"rfcodes"')
    return str;

def mainLoop():
    # field1 - nieuzywane
    # field2 - pir 666
    data = {'field1': 0, 'field2': 0}
    lastTime = time.time()
    buzz_mod = 0
    while 1 :
        out = ''
        time.sleep(0.5)
        while ser.inWaiting() > 0:
            out += ser.read(1)

        if out != '':
           #print (">>" + out)
           if out.find('{msgid') >= 0:
                out = fixJson(out)
                decodeMessage(out, data)

        if((time.time() - lastTime) > thingspeak_delay):
            thingspeak.send(data);
            for z in data.keys():
                if z.startswith('field'):
                    data[z] = 0;
            GPIO.output(buzzer_pin, False)
            lastTime = time.time()
            
        if(data['field2'] == 1):
            if buzz_mod == 1:
                buzz_mod = 0
                GPIO.output(buzzer_pin, False)
            else:
                buzz_mod = 1
                GPIO.output(buzzer_pin, True)


def main():
    init()
    mainLoop()

if __name__ == "__main__":
    sys.exit(int(main() or 0))
