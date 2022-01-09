using TroykaCap.Expander;

namespace SmartFan
{
    public class ServerOptions
    {
        public GpioExpanderPin PinNumber { get; set; }
        public int Freq { get; set; }
        public string FileName { get; set; }
        public int TimeSendingData { get; set; }
    }
}
