using System.Threading.Tasks;

namespace PingDong.DomainDriven.Service
{
    public interface ISingleQuery<T>
    {
        Task<T> GetByIdAsync(int id);
    }
}
