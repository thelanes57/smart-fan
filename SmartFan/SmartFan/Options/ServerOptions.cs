using TroykaCap.Expander;

namespace SmartFan
{
    public class ServerOptions
    {
        public GpioExpanderPin PinPinNumber { get; set; }
        public int Freq { get; set; }
        public string FileName { get; set; }
        public int TimeSendigData { get; set; }
    }
}
