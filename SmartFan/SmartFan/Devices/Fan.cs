using SmartFan.Data;
using TroykaCap.Expander;
using TroykaCap.Expander.Extensions;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace SmartFan.Devices
{
    public class Fan : Device
    {
        public ServerOptions serverOptions { get;}
        private ushort Freq = ServerOptions.Freq;
        private readonly GpioExpander Expander;

        public Fan(string id) : base(id)
        {
            Pi.Init<BootstrapWiringPi>();
            Expander = Pi.I2C.GetGpioExpander();
            Expander.PwmFreq();
        }

        public override double Read()
        {
            return 0;
        }

        public override void Write(ChangeParameter parameter)
        {
            Expander.AnalogWriteDouble(GpioExpanderPin.Pin0, parameter.DutyCycle);
        }
    }
}