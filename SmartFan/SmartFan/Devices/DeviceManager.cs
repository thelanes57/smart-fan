using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using SmartFan.Data;
using SmartFan.Devices;
using SmartFan.Hubs;
using System;
using System.Timers;

namespace SmartFan.Device
{
    public sealed class DeviceManager : IDisposable
    {
        public ChangeParameter ChangeParameter { get; private set; }
        private readonly Term _term;
        private readonly Barom _barom;
        private readonly Gigrom _gigrom;
        private readonly Fan _fan;
        private bool disposed = false;
        private readonly IHubContext<DataHub, IDataHub> _hub;

        private readonly Timer aTimer;

        public DeviceManager(IHubContext<DataHub, IDataHub> hub, IOptionsMonitor<ServerOptions> options)
        {
            _hub = hub;
            _term = new Term("Some name term");
            _barom = new Barom("Some name Barom");
            _gigrom = new Gigrom("Some name Gigrom");
            _fan = new Fan("Some name fan", options);

            aTimer = new Timer(new TimeSpan(0, 0, options.CurrentValue.TimeSendigData).TotalMilliseconds);
            aTimer.Elapsed += GetData;
            aTimer.Start();
        }

        private async void GetData(object source, ElapsedEventArgs args)
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
            _fan.Write(parameter);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                aTimer.Dispose();
            }

            aTimer.Elapsed -= GetData;

            _fan!.Write(0.0);

            disposed = true;
        }

        ~DeviceManager()
        {
            Dispose(disposing: false);
        }
    }
}
