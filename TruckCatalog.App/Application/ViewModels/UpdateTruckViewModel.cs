using System;
using TruckCatalog.App.Core.DomainObjects;
using TruckCatalog.App.Core.Enuns;
using TruckCatalog.App.Models;

namespace TruckCatalog.App.Aplication.ViewModels
{
    public class UpdateTruckViewModel
    {
        public Guid Id { get; set; }                

        public EnunModels Model { get; set; }       

        public int ModelYear { get; set; }        
    }
}
