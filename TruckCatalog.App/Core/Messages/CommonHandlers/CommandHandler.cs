using System.Threading.Tasks;
using FluentValidation.Results;
using TruckCatalog.App.Core.Data;

namespace GCI.Core.Messages.CommonHandlers
{
    public class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
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