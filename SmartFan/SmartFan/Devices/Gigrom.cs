using SmartFan.Data;
using System;

namespace SmartFan.Devices
{
    public class Gigrom : Device
    {
        private readonly Random random = new Random();
        public Gigrom(string id) : base(id)
        {
        }

        public override double Read()
        {
            return random.Next(0, 101) + random.NextDouble();
        }

        public override void Write(ChangeParameter parameter)
        {
            throw new NotSupportedException();
        }
    }
}
