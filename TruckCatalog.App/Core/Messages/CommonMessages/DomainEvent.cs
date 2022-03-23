using MediatR;
using System;
using TruckCatalog.App.Core.Helpers;

namespace TruckCatalog.App.Core.Messages.CommonMessages
{
    public abstract class DomainEvent : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent()
        {
            Timestamp = BrasiliaDateTime.Get();
        }
    }
}
