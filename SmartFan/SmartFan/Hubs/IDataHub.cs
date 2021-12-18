using System.Threading.Tasks;

namespace SmartFan.Hubs
{
    public interface IDataHub
    {
        Task Recever(string message);
    }
}
