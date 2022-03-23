using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckCatalog.App.Models;

namespace TruckCatalog.App.Aplication.Query
{
    public class TruckQuery : ITruckQuery
    {
        private readonly ITruckRepository _truckRepository;

        public TruckQuery(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        public async Task<Truck> GetById(Guid id)
        {
            return await _truckRepository.GetById(id);
        }        

        public async Task<IEnumerable<Truck>> GetAll()
        {
            return await _truckRepository.GetAll();
        }
    }
}