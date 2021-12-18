using SmartFan.Data;
using System.Threading.Tasks;

namespace SmartFan.Hubs
{
    public interface IDataHub
    {
        Task Recever(ParameterValues message);
    }
}
