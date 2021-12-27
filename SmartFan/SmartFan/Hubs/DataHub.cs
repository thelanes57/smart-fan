using Microsoft.AspNetCore.SignalR;
using SmartFan.Data;
using SmartFan.Device;
using System.Threading.Tasks;

namespace SmartFan.Hubs
{
    public class DataHub : Hub<IDataHub>
    {
        DeviceManager deviceManager;
        public DataHub(DeviceManager devManager)
        {
            this.deviceManager = devManager;
        }

        public async Task ReceiveData(ChangeParameter parameter)
        {
            deviceManager.SetData(parameter);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.StartSpeed(deviceManager.СurrentSpeed);
            await base.OnConnectedAsync();
        }
    }
}
