using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingDong.Service
{
    public interface IQuery<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}
