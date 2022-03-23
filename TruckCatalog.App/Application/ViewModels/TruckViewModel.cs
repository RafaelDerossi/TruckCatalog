using System;
using TruckCatalog.App.Core.DomainObjects;
using TruckCatalog.App.Core.Enuns;
using TruckCatalog.App.Models;

namespace TruckCatalog.App.Aplication.ViewModels
{
    public class TruckViewModel
    {
        public Guid Id { get; set; }

        public string RegistrationDate { get; set; }

        public string ChangeDate { get; set; }

        public EnunModels Model { get; set; }

        public string ModelDescription 
        {
            get
            {
                return Model switch
                {
                    EnunModels.FH => "FH",
                    EnunModels.FM => "FM",
                    _ => "",
                };
            }
            
        }

        public int ManufactureYear { get; set; }

        public int ModelYear { get; set; }



        public static TruckViewModel Mapear(Truck truck)
        {
            return new TruckViewModel
            {
                Id = truck.Id,                
                RegistrationDate = truck.FormattedRegistrationDate,                
                ChangeDate = truck.FormattedChangeDate,
                Model = truck.Model,
                ManufactureYear = truck.ManufactureYear,
                ModelYear = truck.ModelYear
            };
        }

    }
}
