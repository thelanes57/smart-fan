using Microsoft.Extensions.Options;
using SmartFan.Data;
using System;
using TroykaCap.Expander;
using TroykaCap.Expander.Extensions;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace SmartFan.Devices
{
    public class Fan : Device
    {
        private readonly GpioExpander Expander;
        private IOptionsMonitor<ServerOptions> _options;
        public Fan(string id, IOptionsMonitor<ServerOptions> options) : base(id)
        {
            _options = options;
            Pi.Init<BootstrapWiringPi>();
            Expander = Pi.I2C.GetGpioExpander();
            Expander.PwmFreq(_options.CurrentValue.Freq);
            
        }

        public override double Read()
        {
            throw new NotSupportedException();
        }

        public override void Write(ChangeParameter parameter)
        {
           Expander.AnalogWriteDouble(_options.CurrentValue.PinPinNumber, parameter.DutyCycle);
        }
    }
}