using System.Threading.Tasks;
using PingDong.DomainDriven.Core;

namespace PingDong.Newmoon.Events.Core
{
    public interface IPlaceRepository : IRepository<Place>
    {
        Task<Place> FindByNameAsync(string name);
        Task<Place> GetByIdAsync(int id);
        Place Add(Place place);
    }
}
