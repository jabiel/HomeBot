import RPi.GPIO as GPIO
import time

class Buzzer(object):
    """Make sound from buzzer connecteed to rpi GPIO"""

    def __init__(self, buzzerPin):
        self._lastState = 0
        self._buzzStateChangesToStop = 0
        self._buzzState = False
        self._buzzerPin = buzzerPin
        self._idleTime = 20 # po ilu sek moze sie ponownie wlaczyc 
        self._lastStartTime = 0.0
        GPIO.setmode(GPIO.BOARD)
        GPIO.setup(self._buzzerPin, GPIO.OUT)
        GPIO.output(self._buzzerPin, False)

    def BuzzOnChange(self, activateState, buzzCount):
        if activateState & ((self._lastStartTime + self._idleTime) < time.time()):
            self._buzzStateChangesToStop = buzzCount * 2
            self._lastStartTime = time.time()

        if self._buzzStateChangesToStop > 0:
            self._buzzState = not self._buzzState;
            #print('buzz: {0} {1}'.format(self._buzzStateChangesToStop, self._buzzState))
            GPIO.output(self._buzzerPin, self._buzzState)
            self._buzzStateChangesToStop -= 1;
