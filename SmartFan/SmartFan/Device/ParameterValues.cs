using SmartFan.Data;
using System;

namespace SmartFan.Devices
{
    public class ParameterValues : Device
    {
        public double TarmValueC { get; set; }
        public double TarmValueF { get; set; }
        public int BarValue { get; set; }
        public int GigValue { get; set; }
        public double DutyCycle { get; set; }

    }
}
