using TruckCatalog.App.Core.Mediator;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation.Results;
using TruckCatalog.App.Aplication.Commands;
using TruckCatalog.App.Aplication.Query;
using TruckCatalog.App.Models;
using TruckCatalog.App.Data.Repository;


namespace TruckCatalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            services.AddScoped<IMediatorHandler, MediatorHandler>();            

            //Truck
            services.AddScoped<IRequestHandler<AddTruckCommand, ValidationResult>, TruckCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTruckCommand, ValidationResult>, TruckCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteTruckCommand, ValidationResult>, TruckCommandHandler>();


            //Query
            services.AddScoped<ITruckQuery, TruckQuery>();            
            

            //Repositório            
            services.AddScoped<ITruckRepository, TruckRepository>();
        }
    }
}
