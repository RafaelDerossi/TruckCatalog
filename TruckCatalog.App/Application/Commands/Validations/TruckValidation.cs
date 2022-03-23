using FluentValidation;
using System;
using TruckCatalog.App.Aplication.Commands;
using TruckCatalog.App.Core.Helpers;

namespace TruckCatalog.App.Application.Commands.Validations
{
    public abstract class TruckValidation<T> : AbstractValidator<T> where T : TruckCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }       
        
        protected void ValidateModel()
        {
            RuleFor(c => c.Model)
                .IsInEnum().WithMessage("Model invalid!");
        }               

        protected void ValidateModelYear()
        {
            RuleFor(c => c.ModelYear)
                .InclusiveBetween(BrasiliaDateTime.Get().Year, BrasiliaDateTime.Get().Year + 1)
                .WithMessage("Model Year must be this year or next year!");
        }
    }
}
