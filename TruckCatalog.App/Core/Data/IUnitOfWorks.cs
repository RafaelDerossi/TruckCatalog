using System.Threading.Tasks;

namespace TruckCatalog.App.Core.Data
{
    public interface IUnitOfWorks
    {
        Task<bool> Commit();
    }
}