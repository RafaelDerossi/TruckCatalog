using System.Collections.Generic;
using System.Threading.Tasks;
using TruckCatalog.App.Core.Data;

namespace TruckCatalog.App.Models
{
    public interface ITruckRepository : IRepository<Truck>
    {
        Task<IEnumerable<Truck>> GetAll();
    }
}
