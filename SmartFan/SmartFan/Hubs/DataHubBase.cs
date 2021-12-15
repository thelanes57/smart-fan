using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SmartFan.Hubs
{
    public abstract class DataHubBase : Hub<IDataHub>
    {
        public abstract Task ReciveData(string message);
    }
}
