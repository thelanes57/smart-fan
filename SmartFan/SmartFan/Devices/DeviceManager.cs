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
<<<<<<< HEAD
        private Barom _barom;
        private Gigrom _gigrom;
        //private Fan _fan;

        private readonly IHubContext<DataHub, IDataHub> _hub;

        public DeviceManager(IHubContext<DataHub, IDataHub> hub)
        {
            _hub = hub;
            _term = new Term("Some name term");
            _barom = new Barom("Some name Barom");
            _gigrom = new Gigrom("Some name Gigrom");
=======
        //private Fan _fan;

        private readonly IHubContext<DataHubBase, IDataHub> _hub;

        public DeviceManager(IHubContext<DataHubBase, IDataHub> hub)
        {
            _hub = hub;
            _term = new Term("Some name term");
>>>>>>> origin/Server
            //_fan = new Fan("Some name fan");
            Thread t = new Thread(GetData);
            t.Start();
        }

        public async void GetData()
        {
            while (true)
            {
<<<<<<< HEAD
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
                await _hub.Clients.All.Recever(data);
=======
                await _hub.Clients.All.Recever(_term.Read().ToString());
>>>>>>> origin/Server
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
