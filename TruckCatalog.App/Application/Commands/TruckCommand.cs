using System;
using TruckCatalog.App.Core.Enuns;
using TruckCatalog.App.Core.Messages.CommonMessages;

namespace TruckCatalog.App.Aplication.Commands
{
    public abstract class TruckCommand : Command
    {
        public Guid Id { get; protected set; }

        public EnunModels Model { get; protected set; }        

        public int ModelYear { get; protected set; }



        public void SetModel(EnunModels model) => Model = model;

        public void SetModelYear(int year) => ModelYear = year;
    }
}
