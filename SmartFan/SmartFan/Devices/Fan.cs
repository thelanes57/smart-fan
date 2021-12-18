<<<<<<< HEAD
﻿using SmartFan.Data;
using TroykaCap.Expander;
using TroykaCap.Expander.Extensions;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace SmartFan.Devices
{
    public class Fan : Device
    {
        private ushort Freq = 2000;
        private readonly GpioExpander Expander;

        public Fan(string id) : base(id)
        {
            Pi.Init<BootstrapWiringPi>();
            Expander = Pi.I2C.GetGpioExpander();
        }

        public override double Read()
        {
            return 0;
        }

        public override void Write(ChangeParameter parameter)
        {
            Expander.PwmFreq(Freq);
            Expander.AnalogWriteDouble(GpioExpanderPin.Pin0, parameter.DutyCycle);

        }
    }
}
=======
﻿//using SmartFan.Data;
//using TroykaCap.Expander;
//using TroykaCap.Expander.Extensions;
//using Unosquare.RaspberryIO;
//using Unosquare.WiringPi;

//namespace SmartFan.Devices
//{
//    public class Fan : Device
//    {
//        private ushort Freq = 2000;
//        private readonly GpioExpander Expander;

//        public Fan(string id) : base(id)
//        {
//            Pi.Init<BootstrapWiringPi>();
//            Expander = Pi.I2C.GetGpioExpander();
//        }

//        public override double Read()
//        {
//            return 0;
//        }

//        public override void Write(ChangeParameter parameter)
//        {
//            Expander.PwmFreq(Freq);
//            Expander.AnalogWriteDouble(GpioExpanderPin.Pin0, parameter.DutyCycle);
//        }
//    }
//}
>>>>>>> origin/Server
