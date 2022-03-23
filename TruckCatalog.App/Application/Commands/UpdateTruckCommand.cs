using System;
using TruckCatalog.App.Application.Commands.Validations;
using TruckCatalog.App.Core.Enuns;

namespace TruckCatalog.App.Aplication.Commands
{
    public class UpdateTruckCommand : TruckCommand
    {
        public UpdateTruckCommand(Guid id, EnunModels model, int modelYear)
        {
            Id = id;
            Model = model;            
            ModelYear = modelYear;
        }


        public override bool IsValid()
        {
            if (!ValidationResult.IsValid)
                return ValidationResult.IsValid;

            ValidationResult = new UpdateTruckCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }


        public class UpdateTruckCommandValidation : TruckValidation<UpdateTruckCommand>
        {
            public UpdateTruckCommandValidation()
            {
                ValidateId();
                ValidateModel();                
                ValidateModelYear();
            }
        }

    }
}
