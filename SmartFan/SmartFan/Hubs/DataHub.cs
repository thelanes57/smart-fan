using SmartFan.Device;
using System;
using System.Threading.Tasks;

namespace SmartFan.Hubs
{
    public class DataHub : DataHubBase
    {
        DeviceManager _dm;
        public DataHub(DeviceManager dm)
        {
            _dm = dm;
        }

        public override async Task ReciveData(string message)
        {
            _dm.SetData(Convert.ToDouble(message));
        }
    }
}
