using System;
using System.Collections.Generic;
using FluentValidation.Results;
using TruckCatalog.App.Core.Helpers;
using TruckCatalog.App.Core.Messages.CommonMessages;

namespace TruckCatalog.App.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public DateTime ChangeDate { get; private set; }
        public bool Garbage { get; private set; }

        protected ValidationResult ValidationResult { get; private set; } = new ValidationResult();


        private List<DomainEvent> _notifications;
        public IReadOnlyCollection<DomainEvent> Notifications => _notifications?.AsReadOnly();

        public void AddDomainEvent(DomainEvent eventItem)
        {
            _notifications = _notifications ?? new List<DomainEvent>();
            _notifications.Add(eventItem);
        }
        public void RemoveDomainEvent(DomainEvent eventItem)
        {
            _notifications?.Remove(eventItem);
        }
        public void ClearDomainEvents()
        {
            _notifications?.Clear();
        }



        protected void AddEntityErrors(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty,mensagem));
        }

        public string FormattedChangeDate
        {
            get
            {
                if (ChangeDate != null)
                    return ChangeDate.ToString("dd/MM/yyyy HH:mm");
                else
                    return null;
            }
        }

        public string FormattedRegistrationDate
        {
            get
            {
                if (RegistrationDate != null)
                    return RegistrationDate.ToString("dd/MM/yyyy HH:mm");
                else
                    return null;
            }
        }

        public void SetEntidadeId(Guid NovoId) => Id = NovoId;

        public void SendToGarbage() => Garbage = true;

        public void RestoreFromGarbage() => Garbage = false;

        public Entity()
        {
            Id = Guid.NewGuid();
            RegistrationDate = BrasiliaDateTime.Get();
            ChangeDate = BrasiliaDateTime.Get();
        }

        #region Comparisons

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        #endregion
    }
}
