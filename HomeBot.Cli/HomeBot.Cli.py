#!/usr/bin/env python
import sys, time, datetime
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
        self.logger = logger
        self.level = level

    def write(self, message):
        if message.rstrip() != "":
            self.logger.log(self.level, message.rstrip())

sys.stdout = MyLogger(logger, logging.INFO)
sys.stderr = MyLogger(logger, logging.ERROR)

"""Motion sensors are from 11 to 19"""
def hasMotionSensors(data):
    for i in range(11,20):
        if "sensor" + str(i) in data.keys():
            return True
    return False

"""At night buzz will take longer to wake up ewerynody """
def buzzOnNight():
    hour = time.localtime(time.time()).tm_hour
    #print('hour: {0}'.format(hour))
    if 1 < hour < 6:
        return 3
    return 1

def main():
    
    ino = utils.Arduino.ArduinoSerial('/dev/ttyUSB0')
    buzz = utils.Buzzer.Buzzer(buzzer_pin)
    lastTime = time.time()
    while 1 :
        time.sleep(0.5)
        sensors = ino.readFromSerial()
        buzz.BuzzOnChange(hasMotionSensors(sensors), buzzOnNight())
        # mapowanie z sensorXX na fieldXX z thingspeak:
        thingSpeakData = {}
        if "sensor11" in sensors.keys():
            thingSpeakData["field2"] = 1    

        thingspeak.sendIfAvailable(thingSpeakData)
        

if __name__ == "__main__":
    sys.exit(int(main() or 0))
