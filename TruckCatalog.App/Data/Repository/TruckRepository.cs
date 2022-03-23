using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TruckCatalog.App.Core.Data;
using TruckCatalog.App.Models;

namespace TruckCatalog.App.Data.Repository
{
    public class TruckRepository : ITruckRepository
    {
        private readonly TruckContextDB _context;
       
        public TruckRepository(TruckContextDB context)
        {
            _context = context;
        }

        public IUnitOfWorks UnitOfWork => _context;



        public async Task<Truck> GetById(Guid Id)
        {
            return await _context.Trucks
                .FirstOrDefaultAsync(u => u.Id == Id && !u.Garbage);            
        }

        public async Task<IEnumerable<Truck>> GetAll()
        {
            return await _context.Trucks.Where(x => !x.Garbage).ToListAsync();
        }

        public async Task<IEnumerable<Truck>> Get(Expression<Func<Truck, bool>> expression, bool OrderByDesc = false, int take = 0)
        {
            if (OrderByDesc)
            {
                if (take > 0)
                    return await _context.Trucks
                                            .AsNoTracking()
                                            .Where(expression)
                                            .OrderByDescending(x => x.RegistrationDate)
                                            .Take(take)
                                            .ToListAsync();

                return await _context.Trucks
                                        .AsNoTracking()
                                        .Where(expression)
                                        .OrderByDescending(x => x.RegistrationDate)
                                        .ToListAsync();
            }

            if (take > 0)
                return await _context.Trucks
                                        .AsNoTracking()
                                        .Where(expression)
                                        .OrderBy(x => x.RegistrationDate)
                                        .Take(take)
                                        .ToListAsync();

            return await _context.Trucks
                                    .AsNoTracking()
                                    .Where(expression)
                                    .OrderBy(x => x.RegistrationDate)
                                    .ToListAsync();
        }


        public void Add(Truck entity)
        {
            _context.Trucks.Add(entity);
        }

        public void Update(Truck entity)
        {
            _context.Trucks.Update(entity);
        }

        public void Delete(Func<Truck, bool> predicate)
        {
            _context.Trucks.Where(predicate).ToList().ForEach(del => del.SendToGarbage());
        }



        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
