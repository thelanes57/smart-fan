using Microsoft.Extensions.Options;
using SmartFan.Data;
using TroykaCap.Expander;
using TroykaCap.Expander.Extensions;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace SmartFan.Devices
{
    public class Fan : Device
    {
        private readonly GpioExpander Expander;
        private IOptions<ServerOptions> _options;
        public Fan(string id, IOptions<ServerOptions> options) : base(id)
        {
            _options = options;
            Pi.Init<BootstrapWiringPi>();
            Expander = Pi.I2C.GetGpioExpander();
            Expander.PwmFreq(_options.Value.Freq);
            
        }

        public override double Read()
        {
            return 0;
        }

        public override void Write(ChangeParameter parameter)
        {
           Expander.AnalogWriteDouble(_options.Value.PinPinNumber, parameter.DutyCycle);
        }
    }
}