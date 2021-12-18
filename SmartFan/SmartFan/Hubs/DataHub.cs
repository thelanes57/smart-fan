using Microsoft.AspNetCore.SignalR;
using SmartFan.Device;
using System;
using System.Threading.Tasks;

namespace SmartFan.Hubs
{
    public class DataHub : Hub<IDataHub>
    {
        DeviceManager _dm;
        public DataHub(DeviceManager dm)
        {
            _dm = dm;
        }

        public async Task ReciveData(string message)
        {
            _dm.SetData(Convert.ToDouble(message));
        }
    }
}
