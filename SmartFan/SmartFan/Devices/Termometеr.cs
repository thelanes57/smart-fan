using SmartFan.Data;
using System;

namespace SmartFan.Devices
{
    public class Termometеr : Device
    {
        private readonly Random random = new Random();
        public Termometеr(string id) : base(id)
        {
        }

        public override double Read()
        {
            return random.Next(-50, 51) + random.NextDouble();
        }

        public override void Write(ChangeParameter parameter)
        {
            throw new NotSupportedException();
        }
    }
}
