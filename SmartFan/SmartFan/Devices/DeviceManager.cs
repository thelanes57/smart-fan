using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using SmartFan.Data;
using SmartFan.Devices;
using SmartFan.Hubs;
using System;
using System.Timers;

namespace SmartFan.Device
{
    public class DeviceManager
    {
        public ChangeParameter ChangeParameter;
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

            aTimer = new Timer(new TimeSpan(0, 0, options.CurrentValue.TimeSendigData).TotalMilliseconds);
            aTimer.Elapsed += GetData;
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
            await _hub.Clients.All.ReceiverDataFromServer(data);
        }

        public void SetData(ChangeParameter parameter)
        {
            ChangeParameter = parameter;
            parameter.DutyCycle = parameter.CurrentSpeed / 100.0;
            //_fan.Write(parameter);
        }
    }
}
