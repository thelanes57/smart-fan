using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using SmartFan.Data;
using SmartFan.Devices;
using SmartFan.Hubs;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace SmartFan.Device
{
    public class DeviceManager
    {
        public int СurrentSpeed;
        private static Term _term;
        private static Barom _barom;
        private static Gigrom _gigrom;
        private static Fan _fan;

        private static IHubContext<DataHub, IDataHub> _hub;

        private static Timer aTimer;

        public DeviceManager(IHubContext<DataHub, IDataHub> hub, IOptionsMonitor<ServerOptions> options)
        {
            _hub = hub;
            _term = new Term("Some name term");
            _barom = new Barom("Some name Barom");
            _gigrom = new Gigrom("Some name Gigrom");
            //_fan = new Fan("Some name fan", options);
            SetTimer(options.CurrentValue.TimeSendigData);
        }

        private static void SetTimer(TimeSpan second)
        {
            aTimer = new Timer(second.TotalMilliseconds);
            aTimer.Elapsed += GetData;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static async void GetData(Object source, ElapsedEventArgs e)
        {
            var valeTem = _term.Read();
            var valeBar = _barom.Read();
            var data = new ParameterValues()
            {
                TarmValueC = valeTem,
                TarmValueF = 9 / 5 * valeTem + 32,
                BarValueMGH = (int)valeBar,
                BarValuePascal = (int)valeBar * 101325 / 760,
                GigValue = (int)_gigrom.Read()
            };
            await _hub.Clients.All.Receiver(data);
        }


        public void SetData(ChangeParameter parameter)
        {
            СurrentSpeed = Convert.ToInt32(parameter.DutyCycle);
            //_fan.Write(new ChangeParameter() { DutyCycle = parameter.DutyCycle / 100 });
        }
    }
}
