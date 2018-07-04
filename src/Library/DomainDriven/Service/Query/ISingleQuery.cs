using System.Threading.Tasks;

namespace PingDong.Service
{
    public interface ISingleQuery<T>
    {
        Task<T> GetByIdAsync(int id);
    }
}
