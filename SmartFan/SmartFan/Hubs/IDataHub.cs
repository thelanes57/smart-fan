using SmartFan.Data;
using System.Threading.Tasks;

namespace SmartFan.Hubs
{
    public interface IDataHub
    {
        Task ReceiverDataFromServer(ParameterValues data);
        Task StartSpeed(ChangeParameter speed);
        Task Shutdown(bool result);
    }
}
