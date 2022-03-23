using System;
using FluentValidation.Results;
using MediatR;

namespace TruckCatalog.App.Core.Messages.CommonMessages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }

        public void AddCommandErrors(string errorMessage)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty,errorMessage));
        }
    }
}