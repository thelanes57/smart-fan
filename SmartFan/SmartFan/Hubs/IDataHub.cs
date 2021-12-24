using SmartFan.Data;
using System.Threading.Tasks;

namespace SmartFan.Hubs
{
    public interface IDataHub
    {
        Task Receiver(ParameterValues message);
        Task StartSpeed(int DutyCycle);
    }
}
