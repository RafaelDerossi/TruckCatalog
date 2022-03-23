using System;
using TruckCatalog.App.Application.Commands.Validations;

namespace TruckCatalog.App.Aplication.Commands
{
    public class DeleteTruckCommand : TruckCommand
    {
        public DeleteTruckCommand(Guid id)
        {
            Id = id;
        }


        public override bool IsValid()
        {
            if (!ValidationResult.IsValid)
                return ValidationResult.IsValid;

            ValidationResult = new DeleteTruckCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }


        public class DeleteTruckCommandValidation : TruckValidation<DeleteTruckCommand>
        {
            public DeleteTruckCommandValidation()
            {
                ValidateId();
            }
        }

    }
}
