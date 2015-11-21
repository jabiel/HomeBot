using HomeBot.Pi.Modules.ArduinoMessageReceiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBot.Tests.Fakes
{
    class FakeSerailCommunicator : ISerialCommunicator
    {
        public string ReadLineMessage { get; set; }
        public FakeSerailCommunicator(string msg = "{msgid: 1, rfcodes:[666]}")
        {
            ReadLineMessage = msg;
        }
        public void Close()
        {
        }

        public void Dispose()
        {
            
        }

        public void Open()
        {
            
        }

        public string ReadLine()
        {
            return ReadLineMessage;
        }
    }
}
