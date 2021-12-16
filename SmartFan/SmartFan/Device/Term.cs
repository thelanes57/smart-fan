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
            return random.Next(-50, 51) + random.NextDouble();
        }

        public override void Write()
        {
            
        }
    }
}