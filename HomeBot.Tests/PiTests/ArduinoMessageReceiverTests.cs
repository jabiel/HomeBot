using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBot.Tests.Fakes;
using HomeBot.Pi.Modules.ArduinoMessageReceiver;

namespace HomeBot.Tests
{
    [TestClass]
    public class ArduinoMessageReceiverTests
    {
        [TestMethod]
        public void ReadAlarmMessage()
        {
            var module = new ArduinoMessageReceiver(new FakeSerailCommunicator("{msgid: 1, rfcodes:[666]}"));
            var obj = module.WaitForArduinoMessage();

            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.MsgId > 0);
            Assert.IsNotNull(obj.Rfcodes);
            Assert.AreEqual(1, obj.Rfcodes.Count());

        }

        [TestMethod]
        public void ReadEmptyMessage()
        {
            var module = new ArduinoMessageReceiver(new FakeSerailCommunicator("{msgid: 1, rfcodes:[]}"));
            var obj = module.WaitForArduinoMessage();

            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.MsgId > 0);
            Assert.IsNotNull(obj.Rfcodes);
            Assert.AreEqual(0, obj.Rfcodes.Count());
        }
    }
}
