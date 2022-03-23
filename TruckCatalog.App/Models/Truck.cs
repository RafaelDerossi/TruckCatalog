using System;
using TruckCatalog.App.Core.DomainObjects;
using TruckCatalog.App.Core.Enuns;
using TruckCatalog.App.Core.Helpers;

namespace TruckCatalog.App.Models
{
    public class Truck : Entity, IAggregateRoot
    {
        public EnunModels Model { get; private set; }

        public int ManufactureYear { get; private set; }

        public int ModelYear { get; private set; }

        public Truck()
        {
        }

        public Truck(EnunModels model, int modelYear)
        {
            Model = model;            
            ModelYear = modelYear;
            ManufactureYear = BrasiliaDateTime.Get().Year;
        }


        public void SetModel(EnunModels model) => Model = model;        

        public void SetModelYear(int year) => ModelYear = year;

    }
}
