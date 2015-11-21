using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBot.Pi.Modules.ArduinoMessageReceiver
{
    // zbiera informacje z arduino
    public class ArduinoMessageReceiver
    {
        ISerialCommunicator _serail;
        public ArduinoMessageReceiver(ISerialCommunicator serail)
        {
            _serail = serail;
        }

        public ArduinoMessageModel WaitForArduinoMessage()
        {
            var json = _serail.ReadLine();
            Console.WriteLine(">" + json + "<");
            if (json.StartsWith("{"))
            {
                var obj = JsonConvert.DeserializeObject<ArduinoMessageModel>(json);
                return obj;
            }
            return null;
        }

        public void Loop()
        {
            _serail.Open();
            while (true)
            {
                var obj = WaitForArduinoMessage();
                if (obj != null)
                {
                    Console.WriteLine("+ MsgId:" + obj.MsgId);
                    if (obj.Rfcodes != null)
                        foreach (var c in obj.Rfcodes)
                            Console.WriteLine("+ code:" + c);

                }
            }
        }
    }

    public class ArduinoMessageModel
    {
        public int MsgId { get; set; }
        public IEnumerable<int> Rfcodes { get; set; }
    }
}
