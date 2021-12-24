using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SmartFan.Device;
using System;
using System.Threading.Tasks;

namespace SmartFan.Hubs
{
    public class DataHub : Hub<IDataHub>
    {
        DeviceManager deviceManager;
        private readonly ILogger<DataHub> logger;
        public DataHub(DeviceManager devManager, ILogger<DataHub> logger)
        {
            deviceManager = devManager;
            this.logger = logger;
            
        }

        public async Task ReceiveData(string message)
        {
            logger.LogInformation($"DutyCycle {message}");
            deviceManager.SetData(Convert.ToDouble(message));
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.StartSpeed(deviceManager.currentSpeed);
            await base.OnConnectedAsync();
        }
    }
}
