# HomeBot

Aby nie zapomnieć:

## hbotService
Azure Mobile service zapisujecy eventy do bazyEF Code first z migracja

## hbot
Web app korzystajcy z hbotService. Narazie nie ruszony

## HomeBot.Api
Web api project zapisujcy eventy do bazy (HomneBot.Api.Db) Uzywa DB first

## HomeBot.Pi
Console application odpalony na Raspberry Pi (z zainstaloiwanym Mono)
To ma być core systemu.
Najwazniejsze zadania:
* Odbiera dane z sensorów (które przychodza z sensorow poprzez arduino hub do rpi)
* Wysylanie danych w internet
* Komunikacja z uzytkownikiem - odbieranie danych z netu

## HomeBot.Arduino/HomeBotHub
Projekt na arduino podczony do Raspberry pi po serialu (Usb) sużcy do zbierania informacji nadawanych przez inne arduino po RF433

## HomeBot.Arduino/Sensors
Sensory oparte o arduino. Nadajce bezprzewodowo (RF433). 
Narazie tylko Motion detector.
Uwaga: Aby Sensory byy naprawde low power trzeba dodać tranzystor do wyczania RF433 transmittera gdy arduino jest upione

Sensory dodania:
* Temperature sensor
* Magnetic sensor - Taki do zamontowania na drzwiach/oknach który wysle sygna gdy drzwi sie otworza
* 

