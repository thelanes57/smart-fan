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
        public int currentSpeed;
        private Term _term;
        private Barom _barom;
        private Gigrom _gigrom;
        private Fan _fan;

        private readonly IHubContext<DataHub, IDataHub> _hub;

        public DeviceManager(IHubContext<DataHub, IDataHub> hub)
        {
            _hub = hub;
            _term = new Term("Some name term");
            _barom = new Barom("Some name Barom");
            _gigrom = new Gigrom("Some name Gigrom");
            //_fan = new Fan("Some name fan");
            Thread thread = new Thread(GetData);
            thread.Start();
        }

        public async void GetData()
        {

            while (true)
            {
                var valeTem = _term.Read();
                var valeBar = _barom.Read();
                var data = new ParameterValues()
                {
                    TarmValueC = valeTem,
                    TarmValueF = 9/5 * valeTem + 32,
                    BarValueMGH = (int)valeBar,
                    BarValuePascal = (int)valeBar * 101325/760,
                    GigValue = (int) _gigrom.Read()
                };
                await _hub.Clients.All.Receiver(data);
                await Task.Delay(5000);
            }
        }


        public void SetData(double dutyCycle)
        {
            currentSpeed = Convert.ToInt32(dutyCycle);
            //_fan.Write(new ChangeParameter() { DutyCycle = dutyCycle / 100 });
        }
    }
}
