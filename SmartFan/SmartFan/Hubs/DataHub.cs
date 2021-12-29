using System;
using Microsoft.AspNetCore.SignalR;
using SmartFan.Data;
using SmartFan.Device;
using System.Threading.Tasks;
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

        public async Task<bool> Shotdown()
        {
            try
            {
                await Pi.ShutdownAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public override async Task OnConnectedAsync()
        {
            await Clients.All.StartSpeed(deviceManager.ChangeParameter);
            await base.OnConnectedAsync();
        }
    }
}