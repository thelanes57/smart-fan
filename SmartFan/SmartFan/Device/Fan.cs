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
        
        public Fan(string id) : base(id)
        {
            Pi.Init<BootstrapWiringPi>();
            Expander = Pi.I2C.GetGpioExpander();
        }

        public override double Read()
        {
            return 0;
        }

        public override void Write(ParameterValues parameter)
        {
            Expander.PwmFreq(Freq);
            Expander.AnalogWriteDouble(GpioExpanderPin.Pin0, parameter.DutyCycle);
        }
    }
}
