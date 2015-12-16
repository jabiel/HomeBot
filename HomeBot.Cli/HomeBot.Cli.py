#!/usr/bin/env python
import sys, time
import logging, logging.handlers
import RPi.GPIO as GPIO
import utils.Arduino, utils.Thingspeak, utils.Buzzer

LOG_FILENAME = "/var/log/homebot.log"
LOG_LEVEL = logging.INFO
logger = logging.getLogger(__name__)
logger.setLevel(LOG_LEVEL)
handler = logging.handlers.TimedRotatingFileHandler(LOG_FILENAME, when="midnight", backupCount=3)
formatter = logging.Formatter('%(asctime)s %(levelname)-8s %(message)s')
handler.setFormatter(formatter)
logger.addHandler(handler)
logger.info("Initializing script")

thingspeak = utils.Thingspeak.ThingspeakClient('CHNLDXPXT2S5L19L')
thingspeak_delay = 15
buzzer_pin = 11

class MyLogger(object):
    def __init__(self, logger, level):
        """Needs a logger and a logger level."""
        self.logger = logger
        self.level = level

    def write(self, message):
        if message.rstrip() != "":
            self.logger.log(self.level, message.rstrip())

sys.stdout = MyLogger(logger, logging.INFO)
sys.stderr = MyLogger(logger, logging.ERROR)

def main():
    
    ino = utils.Arduino.ArduinoSerial('/dev/ttyUSB0')
    buzz = utils.Buzzer.Buzzer(buzzer_pin)
    data = {'field1': 0, 'field2': 0}
    buzz_mod = 0
    lastTime = time.time()
    while 1 :
        time.sleep(0.5)        
        ino.readFromSerial(data)

        buzz.BuzzOnChange((data['field2']==1), 1)

        if thingspeak.sendIfAvailableAndChanged(data):
            for z in data.keys():
                if z.startswith('field'):
                    data[z] = 0;
            print('Reset data: {0}'.format(data))
            lastTime = time.time()
        

if __name__ == "__main__":
    sys.exit(int(main() or 0))
