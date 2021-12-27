using SmartFan.Data;

namespace SmartFan.Devices
{
    public abstract class Device
    {
        public string Id { get; }

        public Device(string id)
        {
            Id = id;
        }

        public abstract double Read();

        public abstract void Write(ChangeParameter parameter);
    }
}
