// odbiera informacje z rf433 i przekazuje po serailu do rpi

#include <RCSwitch.h>

RCSwitch mySwitch = RCSwitch();
const int RFPIN = 0; // pin 2 na arduino
const long interval = 1000;
unsigned long previousMillis = 0;
void setup() {
  Serial.begin(9600);
  Serial.print("INFO,Starting Arduino hub..");
  mySwitch.enableReceive(RFPIN);  // Receiver on inerrupt 0 => that is pin #2
}

void loop() {
  unsigned long currentMillis = millis();
  long rfv = ReceiveFromRf();
  if(rfv > 0)
    addToList(rfv);
  
  if (currentMillis - previousMillis >= interval) {
    previousMillis = currentMillis;

    String msg = FormatCsvMsg();
    Serial.println(msg);
  }
}

long codesToSend[100];
int ctsLen = 0;
void addToList(long c)
{
  bool alreadyOnList = false;
  for (int i=0;i<ctsLen;i++)
  {
    if(codesToSend[i] == c)
      alreadyOnList = true;
  }
  if(!alreadyOnList)
  {
    codesToSend[ctsLen] = c;
    ctsLen++;  
  }
}
// obsolete
String FormatJsonMsg()
{
  String s;
  s += "{msgid: " + String(previousMillis >> 10, DEC);
  s += ", rfcodes:[";

  for (int i=0;i<ctsLen;i++)
  {
    if(i>0) s+= ",";
    s += String(codesToSend[i], DEC);
  }
  s+="]}";
  ctsLen = 0; // reset list to not send it next time
  return s;
}

String FormatCsvMsg()
{
  String s = "DATA,";
  s += String(previousMillis >> 10, DEC);
  //s += ",";

  for (int i=0;i<ctsLen;i++)
  {
    //if(i>0) 
    s+= ",";
    s += String(codesToSend[i], DEC);
  }
  ctsLen = 0; // reset list to not send it next time
  return s;
}


long ReceiveFromRf()
{
  long receivedValue = 0;
  if (mySwitch.available()) {
    receivedValue = mySwitch.getReceivedValue();
    mySwitch.resetAvailable();
  }
  
  return receivedValue;
}


