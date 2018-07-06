using System;
using System.Threading.Tasks;

namespace PingDong.DomainDriven.Service
{
    public interface IRequestManager
    {
        Task<bool> CheckExistAsync(Guid id);

        Task CreateRequestForCommandAsync<T>(Guid id);
    }
}
