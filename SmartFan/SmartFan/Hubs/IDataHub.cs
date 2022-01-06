using SmartFan.Data;
using System.Threading.Tasks;

namespace SmartFan.Hubs
{
    public interface IDataHub
    {
        Task ReceiverDataFromServer(ParameterValues message);
        Task StartSpeed(ChangeParameter currentSpeed);
        Task Shutdown(bool result);
    }
}
