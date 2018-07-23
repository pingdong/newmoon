using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingDong.DomainDriven.Service
{
    public interface IQuery<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}
