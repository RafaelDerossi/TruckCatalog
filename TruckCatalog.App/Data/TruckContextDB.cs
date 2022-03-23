using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TruckCatalog.App.Core.Data;
using TruckCatalog.App.Core.Helpers;
using TruckCatalog.App.Core.Mediator;
using TruckCatalog.App.Core.Messages.CommonMessages;
using TruckCatalog.App.Models;
using TruckCatalog.App.Core.Extensions;

namespace TruckCatalog.App.Data
{
    public class TruckContextDB : DbContext, IUnitOfWorks
    {
        private readonly IMediatorHandler _mediatorHandler;     

        public DbSet<Truck> Trucks { get; set; }        
        
        public TruckContextDB(DbContextOptions<TruckContextDB> options,
                   IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<DomainEvent>();            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TruckContextDB).Assembly);
        }

        public async Task<bool> Commit()
        {
            var cetZone = Timezone.GetTimezone();

            foreach (var entry in ChangeTracker.Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("RegistrationDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("RegistrationDate").CurrentValue =
                        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);
                    entry.Property("ChangeDate").CurrentValue =
                        entry.Property("RegistrationDate").CurrentValue;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("RegistrationDate").IsModified = false;
                    entry.Property("ChangeDate").CurrentValue =
                        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);
                }
            }

            var sucesso = await SaveChangesAsync() > 0;
            if (sucesso)
            {
                await _mediatorHandler.PublishDomainEvents(this);                
            }

            return sucesso;
          
        }
    }
}
