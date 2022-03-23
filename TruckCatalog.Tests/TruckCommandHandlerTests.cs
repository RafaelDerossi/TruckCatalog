using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using System.Threading.Tasks;
using TruckCatalog.App.Aplication.Commands;
using TruckCatalog.App.Core.Enuns;
using TruckCatalog.App.Core.Helpers;
using TruckCatalog.App.Models;
using Xunit;

namespace TruckCatalog.Tests
{
    public class TruckCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly TruckCommandHandler _truckCommandHandler;

        public TruckCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _truckCommandHandler = _mocker.CreateInstance<TruckCommandHandler>();
        }

        [Fact(DisplayName = "Add Valid Truck")]
        [Trait("Category", "Truck - TruckCommandHandler")]
        public async Task AddTruck_ValidCommand_MustPass()
        {
            //Arrange
            var Command = TruckCommandFactory.CreateAddTruckCommand();

            //_mocker.GetMock<ITruckRepository>().Setup(r => r.VerificaCodigoJaCadastrado(Command.Codigo))
            //    .Returns(Task.FromResult(false));

            _mocker.GetMock<ITruckRepository>().Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            //Act
            var result = await _truckCommandHandler.Handle(Command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<ITruckRepository>().Verify(r => r.Add(It.IsAny<Truck>()), Times.Once);
            _mocker.GetMock<ITruckRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }        

        [Fact(DisplayName = "Add Invalid Truck - Invalid Model")]
        [Trait("Category", "Truck - TruckCommandHandler")]
        public async Task AddTruck_InvalidCommand_InvalidModel_MustNotPass()
        {
            //Arrange
            var Command = TruckCommandFactory.CreateAddTruckCommand_WithInvalidModel();            

            _mocker.GetMock<ITruckRepository>().Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            //Act
            var result = await _truckCommandHandler.Handle(Command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Add Invalid Truck - Invalid ModelYear")]
        [Trait("Category", "Truck - TruckCommandHandler")]
        public async Task AddTruck_InvalidCommand_InvalidModelYear_MustNotPass()
        {
            //Arrange
            var Command = TruckCommandFactory.CreateAddTruckCommand_WithInvalidModelYear();

            _mocker.GetMock<ITruckRepository>().Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            //Act
            var result = await _truckCommandHandler.Handle(Command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }




        [Fact(DisplayName = "Update Valid Truck")]
        [Trait("Category", "Truck - TruckCommandHandler")]
        public async Task UpdateTruck_ValidCommand_MustPass()
        {
            //Arrange
            var Command = TruckCommandFactory.CreateUpdateTruckCommand();

            var truck = new Truck(EnunModels.FH, BrasiliaDateTime.Get().Year);

            _mocker.GetMock<ITruckRepository>().Setup(r => r.GetById(Command.Id))
                .Returns(Task.FromResult(truck));

            _mocker.GetMock<ITruckRepository>().Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            //Act
            var result = await _truckCommandHandler.Handle(Command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<ITruckRepository>().Verify(r => r.Update(It.IsAny<Truck>()), Times.Once);
            _mocker.GetMock<ITruckRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Update Invalid Truck - Invalid Model")]
        [Trait("Category", "Truck - TruckCommandHandler")]
        public async Task UpdateTruck_InvalidCommand_InvalidModel_MustNotPass()
        {
            //Arrange
            var Command = TruckCommandFactory.CreateAddTruckCommand_WithInvalidModel();

            var truck = new Truck(EnunModels.FH, BrasiliaDateTime.Get().Year);

            _mocker.GetMock<ITruckRepository>().Setup(r => r.GetById(Command.Id))
                .Returns(Task.FromResult(truck));

            _mocker.GetMock<ITruckRepository>().Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            //Act
            var result = await _truckCommandHandler.Handle(Command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Update Invalid Truck - Invalid ModelYear")]
        [Trait("Category", "Truck - TruckCommandHandler")]
        public async Task Truck_InvalidCommand_InvalidModelYear_MustNotPass()
        {
            //Arrange
            var Command = TruckCommandFactory.CreateAddTruckCommand_WithInvalidModelYear();

            _mocker.GetMock<ITruckRepository>().Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            var truck = new Truck(EnunModels.FH, BrasiliaDateTime.Get().Year);

            _mocker.GetMock<ITruckRepository>().Setup(r => r.GetById(Command.Id))
                .Returns(Task.FromResult(truck));

            //Act
            var result = await _truckCommandHandler.Handle(Command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

    }
}
