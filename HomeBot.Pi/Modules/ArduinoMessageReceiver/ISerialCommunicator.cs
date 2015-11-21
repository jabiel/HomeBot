namespace HomeBot.Pi.Modules.ArduinoMessageReceiver
{
    public interface ISerialCommunicator
    {
        void Close();
        void Dispose();
        void Open();
        string ReadLine();
    }
}