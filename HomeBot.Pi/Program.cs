using HomeBot.Pi.Modules.ArduinoMessageReceiver;
using Raspberry.IO.GeneralPurpose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeBot.Pi
{
    class Program
    {
        public const string URL = "http://hbot.azurewebsites.net/api/light";
        private static bool keepRunning = true;

        static void Main(string[] args)
        {
            var exitEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (sender, eventArgs) => {
                eventArgs.Cancel = true;
                exitEvent.Set();
            };

            using (var _serail = new SerialCommunicator("/dev/ttyUSB0"))
            {
                var arduino = new ArduinoMessageReceiver(_serail);
                arduino.Loop();
                //var thread1 = new Thread(new ThreadStart(arduino.Loop));

            }
            


            //var led1 = ConnectorPin.P1Pin07.Output();

            //using (var connection = new GpioConnection(led1))
            //{

            //    while (true)
            //    {
            //        var z = WebApiRequest.Get(URL, null);
            //        if (z == "false" && !connection[led1])
            //        {
            //            connection[led1] = true;
            //            Console.WriteLine("light: off");
            //        }

            //        if (z == "true" && connection[led1])
            //        {
            //            connection[led1] = false;
            //            Console.WriteLine("light: on");
            //        }
            //        System.Threading.Thread.Sleep(2000);
            //    }
            //}
        }
    }
}
