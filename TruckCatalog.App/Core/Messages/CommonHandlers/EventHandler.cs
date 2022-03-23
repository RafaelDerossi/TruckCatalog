using System.Threading.Tasks;
using FluentValidation.Results;
using TruckCatalog.App.Core.Data;

namespace TruckCatalog.App.Core.Messages.CommonHandlers
{
    public class EventHandler
    {
        protected ValidationResult ValidationResult;

        protected EventHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWorks uow)
        {
            try
            {
                if (!await uow.Commit()) AddError("There was an error persisting the data!");
            }
            catch (System.Exception ex)
            {
                AddError(ex.Message);
            }          

            return ValidationResult;
        }
        
        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }
    }
}