using System;
using TruckCatalog.App.Application.Commands.Validations;
using TruckCatalog.App.Core.Enuns;

namespace TruckCatalog.App.Aplication.Commands
{
    public class AddTruckCommand : TruckCommand
    {
        public AddTruckCommand(EnunModels model, int modelYear)
        {
            Model = model;            
            ModelYear = modelYear;
        }


        public override bool IsValid()
        {
            if (!ValidationResult.IsValid)
                return ValidationResult.IsValid;

            ValidationResult = new AddTruckCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }


        public class AddTruckCommandValidation : TruckValidation<AddTruckCommand>
        {
            public AddTruckCommandValidation()
            {                               
                ValidateModel();                
                ValidateModelYear();
            }
        }      

    }
}
