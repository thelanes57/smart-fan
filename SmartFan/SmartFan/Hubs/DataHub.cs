using System;
using Microsoft.AspNetCore.SignalR;
using SmartFan.Data;
using SmartFan.Device;
using System.Threading.Tasks;
using Swan;
using Unosquare.RaspberryIO;

namespace SmartFan.Hubs
{
    public class DataHub : Hub<IDataHub>
    {
        DeviceManager deviceManager;

        public DataHub(DeviceManager devManager)
        {
            this.deviceManager = devManager;
        }

        public async Task ReceiveDataFromClient(ChangeParameter parameter)
        {
            deviceManager.SetData(parameter);
        }

        public async Task Shutdown()
        {
            bool result;
            
            try
            {
                await ProcessRunner.GetProcessResultAsync("/sbin/shutdown", "+1");

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            
            await Clients.All.Shutdown(result);
        }
        
        public override async Task OnConnectedAsync()
        {
            await Clients.All.StartSpeed(deviceManager.ChangeParameter);
            await base.OnConnectedAsync();
        }
    }
}