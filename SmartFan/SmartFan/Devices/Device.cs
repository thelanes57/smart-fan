using SmartFan.Data;

namespace SmartFan.Devices
{
    public abstract class Device
    {
        public string id { get;private set; }

        public Device(string id)
        {
            this.id = id;
        }

        public abstract double Read();

        public abstract void Write(ChangeParameter parameter);
    }
}
