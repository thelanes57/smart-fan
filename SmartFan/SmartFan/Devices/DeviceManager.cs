using Microsoft.AspNetCore.SignalR;
using SmartFan.Data;
using SmartFan.Devices;
using SmartFan.Hubs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartFan.Device
{
    public class DeviceManager
    {
        public int correntSpeed = 10;
        private Term _term;
        //private Fan _fan;

        private readonly IHubContext<DataHubBase, IDataHub> _hub;

        public DeviceManager(IHubContext<DataHubBase, IDataHub> hub)
        {
            _hub = hub;
            _term = new Term("Some name term");
            //_fan = new Fan("Some name fan");
            Thread t = new Thread(GetData);
            t.Start();
        }

        public async void GetData()
        {
            while (true)
            {
                await _hub.Clients.All.Recever(_term.Read().ToString());
                await Task.Delay(5000);
            }
        }

        public void SetData(double dutyCycle)
        {
            correntSpeed = Convert.ToInt32(dutyCycle);
            //_fan.Write(new ChangeParameter() { DutyCycle = dutyCycle });
        }
    }
}
