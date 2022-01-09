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
        private readonly Termometеr term;
        private readonly Barometеr bar;
        private readonly Hygrometer hygr;
        private readonly Fan fan;
        private bool disposed = false;
        private readonly IHubContext<DataHub, IDataHub> _hub;
        private readonly Timer timer;

        public DeviceManager(IHubContext<DataHub, IDataHub> hub, IOptionsMonitor<ServerOptions> options)
        {
            _hub = hub;
            term = new Termometеr("Some name termometer");
            bar = new Barometеr("Some name barometer");
            hygr = new Hygrometer("Some name hygrometer");
            fan = new Fan("Some name fan", options);
            timer = new Timer(new TimeSpan(0, 0, options.CurrentValue.TimeSendingData).TotalMilliseconds);
            timer.Elapsed += GetData;
            timer.Start();
        }

        private async void GetData(object source, ElapsedEventArgs args)
        {
            var termValue = term.Read();
            var barValue = bar.Read();
            var value = new ParameterValues()
            {
                TermValueC = termValue,
                TermValueF = 9 / 5 * termValue + 32,
                BarValueMGH = (int)barValue,
                BarValuePascal = (int)barValue * 101325 / 760,
                HygrValue = (int)hygr.Read()
            };
            await _hub.Clients.All.ReceiverDataFromServer(value);
        }

        public void SetData(ChangeParameter parameter)
        {
            ChangeParameter = parameter;
            parameter.DutyCycle = parameter.Speed / 100.0;
            fan.Write(parameter);
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
                timer.Dispose();
            }
            timer.Elapsed -= GetData;
            fan!.Write(0.0);
            disposed = true;
        }

        ~DeviceManager()
        {
            Dispose(disposing: false);
        }
    }
}
