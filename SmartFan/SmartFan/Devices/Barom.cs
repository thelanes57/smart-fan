using SmartFan.Data;
using System;

namespace SmartFan.Devices
{
    public class Barom : Device
    {
        private readonly Random random = new Random();
        public Barom(string id) : base(id)
        {
        }

        public override double Read()
        {
            return random.Next(700, 801) + random.NextDouble(); //мм рт. ст.
        }

        public override void Write(ChangeParameter parameter)
        {

        }
    }
}
