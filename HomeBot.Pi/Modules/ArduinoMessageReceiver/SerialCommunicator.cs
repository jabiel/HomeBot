using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace HomeBot.Pi.Modules.ArduinoMessageReceiver
{
    public class SerialCommunicator : IDisposable, ISerialCommunicator
    {
        static SerialPort sp;
        public SerialCommunicator(string portName)
        {
            // "/dev/ttyUSB0"
            sp = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
        }


        public void Open()
        {
            sp.Open();
        }

        public void Close()
        {
            sp.Close();
        }

        public string ReadLine()
        {
            string rxString = "";
            byte prevb = 0;
            var b = (byte)sp.ReadByte();

            while (b != 10 || prevb != 13) // 1310 EOL
            {
                //Console.WriteLine(":"+b+" " + (char)b);
                if(b != 10 && b != 13)
                    rxString += ((char)b);
                prevb = b;
                b = (byte)sp.ReadByte();
            }

            return rxString;
        }

        /// <summary>
        /// From http://stackoverflow.com/questions/434494/serial-port-rs232-in-mono-for-multiple-platforms
        /// </summary>
        /// <returns></returns>
        public static List<string> GetPortNames()
        {
            int p = (int)Environment.OSVersion.Platform;
            List<string> serial_ports = new List<string>();

            // Are we on Unix?
            if (p == 4 || p == 128 || p == 6)
            {
                string[] ttys = System.IO.Directory.GetFiles("/dev/", "tty*");
                foreach (string dev in ttys)
                {
                    //Arduino MEGAs show up as ttyACM due to their different USB<->RS232 chips
                    if (dev.StartsWith("/dev/ttyS") || dev.StartsWith("/dev/ttyUSB") || dev.StartsWith("/dev/ttyACM"))
                    {
                        serial_ports.Add(dev);
                    }
                }
            }
            else
            {
                serial_ports.AddRange(SerialPort.GetPortNames());
            }

            return serial_ports;
        }

        public void Dispose()
        {
            Console.WriteLine("Closing SerialCommunicator..");
            sp.Close();
            sp.Dispose();
        }
    }
}
