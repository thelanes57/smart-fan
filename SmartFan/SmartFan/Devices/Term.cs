using SmartFan.Data;
using System;

namespace SmartFan.Devices
{
    public class Term : Device
    {
        private readonly Random random = new Random();
        public Term(string id) : base(id)
        {
        }

        public override double Read()
        {
            return random.Next(10, 35) + random.NextDouble();
        }

        public override void Write(ChangeParameter parameter)
        {
            throw new NotImplementedException();
        }
    }
}
