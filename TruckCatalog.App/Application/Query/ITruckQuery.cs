using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckCatalog.App.Models;

namespace TruckCatalog.App.Aplication.Query
{
    public interface ITruckQuery
    {
        Task<Truck> GetById(Guid Id);        

        Task<IEnumerable<Truck>> GetAll();      
    }
}