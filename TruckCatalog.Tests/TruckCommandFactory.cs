using System;
using TruckCatalog.App.Aplication.Commands;
using TruckCatalog.App.Core.Enuns;
using TruckCatalog.App.Core.Helpers;

namespace TruckCatalog.Tests
{
    public class TruckCommandFactory
    {
        #region AddTruckCommand
        private static AddTruckCommand AddTruckCommandFactoy()
        {
            return new AddTruckCommand(EnunModels.FH, BrasiliaDateTime.Get().Year);
        }


        public static AddTruckCommand CreateAddTruckCommand()
        {
            return AddTruckCommandFactoy();
        }

        public static AddTruckCommand CreateAddTruckCommand_WithInvalidModel()
        {
            var comando = AddTruckCommandFactoy();

            comando.SetModel(0);

            return comando;
        }

        public static AddTruckCommand CreateAddTruckCommand_WithInvalidModelYear()
        {
            var comando = AddTruckCommandFactoy();

            comando.SetModelYear(2020);

            return comando;
        }
        #endregion


        #region UpdateTruckCommand
        private static UpdateTruckCommand UpdateTruckCommandFactoy()
        {
            return new UpdateTruckCommand(Guid.NewGuid(), EnunModels.FH, BrasiliaDateTime.Get().Year);
        }

        public static UpdateTruckCommand CreateUpdateTruckCommand()
        {
            return UpdateTruckCommandFactoy();
        }

        public static UpdateTruckCommand CreateUpdateTruckCommand_WithInvalidModel()
        {
            var comando = UpdateTruckCommandFactoy();

            comando.SetModel(0);

            return comando;
        }

        public static UpdateTruckCommand CreateUpdateTruckCommand_WithInvalidModelYear()
        {
            var comando = UpdateTruckCommandFactoy();

            comando.SetModelYear(2020);

            return comando;
        }

        #endregion



    }
}