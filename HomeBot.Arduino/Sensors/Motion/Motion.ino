#include <JeeLib.h>
#include <RCSwitch.h> // https://code.google.com/p/rc-switch/
#include <PinChangeInt.h>

long sensorId = 11;         // sensor identifier - from 1 to 99
long sensorAlertCode = 1;  // value send by sensor from 1 to 99999

int rfAlertCode = 1;     // must be in 24 bit so max 16777215 , should containd sensor id sensor value 
int calibrationTime = 12;  //the time we give the sensor to calibrate (10-60 secs according to the datasheet)
int rfPacketsToSend = 7;   // to be sure message is received I send messge few times

// Pins
int PIR = 2;
int LED = 13;
int RFTX = 9;   
int RFPOWER = 8;// do tranzystora

RCSwitch rf = RCSwitch();
ISR(WDT_vect) {Sleepy::watchdogEvent();}  // Setup for low power waiting

void setup() {
  Serial.begin(9600);
  pinMode(PIR, INPUT);
  pinMode(LED, OUTPUT);
  pinMode(RFPOWER, OUTPUT);
  rf.enableTransmit(RFTX);

  PRR = bit(PRTIM1);                           // only keep timer 0 going
  ADCSRA &= ~ bit(ADEN); bitSet(PRR, PRADC);   // Disable the ADC to save power
  PCintPort::attachInterrupt(PIR, wakeUp, CHANGE);
  
  CalibratePirSensor();  //give the sensor some time to calibrate
}

void loop() {
  Serial.print("<"); // Motion detected
  digitalWrite(LED, HIGH);
  digitalWrite(RFPOWER, HIGH);// wlacz zasilanie w rf
  delay(100);
  digitalWrite(LED, LOW);
  
  for (int i = 0; i < rfPacketsToSend; i++)
  {
    rf.send((sensorId * 100000) + sensorAlertCode, 24); // it will send 11 00001 where 11 is sensor id and 1 is motion detected
    Serial.print(".");
  }
  digitalWrite(RFPOWER, LOW); // wylacz zasialnie rf
  Serial.println(">");
  delay(100);
  Sleepy::powerDown();
}

void wakeUp() { }

void CalibratePirSensor()
{
  
  Serial.println("Calibrating PIR sensor ");
  for (int i = 0; i < calibrationTime; i++) {
    Serial.print(".");
    digitalWrite(LED, HIGH);
    delay(100);
    digitalWrite(LED, LOW);
    delay(900);
  }
  Serial.println("");
  Serial.println("Sensor active");
  digitalWrite(LED, LOW);
  delay(50);
}
