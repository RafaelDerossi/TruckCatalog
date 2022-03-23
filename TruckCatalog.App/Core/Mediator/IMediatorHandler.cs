using System.Threading.Tasks;
using FluentValidation.Results;
using TruckCatalog.App.Core.Messages.CommonMessages;

namespace TruckCatalog.App.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : DomainEvent;

        Task<ValidationResult> SendCommand<T>(T comando) where T : Command;
    }
}
