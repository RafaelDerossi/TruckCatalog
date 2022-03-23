using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using GCI.Core.Messages.CommonHandlers;
using TruckCatalog.App.Models;

namespace TruckCatalog.App.Aplication.Commands
{
    public class TruckCommandHandler : CommandHandler,
         IRequestHandler<AddTruckCommand, ValidationResult>,
         IRequestHandler<UpdateTruckCommand, ValidationResult>,
         IRequestHandler<DeleteTruckCommand, ValidationResult>,
         IDisposable
    {

        private readonly ITruckRepository _truckRepository;

        public TruckCommandHandler(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        public async Task<ValidationResult> Handle(AddTruckCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var truck = new Truck(request.Model, request.ModelYear);            

            _truckRepository.Add(truck);
            
            return await PersistData(_truckRepository.UnitOfWork);
        }


        public async Task<ValidationResult> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var truck = await _truckRepository.GetById(request.Id);
            if (truck == null)
            {
                AddError("Truck not found!");
                return ValidationResult;
            }

            truck.SetModel(request.Model);            
            truck.SetModelYear(request.ModelYear);

            _truckRepository.Update(truck);

            return await PersistData(_truckRepository.UnitOfWork);
        }

        
        public async Task<ValidationResult> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var truck = await _truckRepository.GetById(request.Id);
            if (truck == null)
            {
                AddError("Truck not found!");
                return ValidationResult;
            }

            _truckRepository.Delete(x => x.Id == truck.Id);

            return await PersistData(_truckRepository.UnitOfWork);
        }


        public void Dispose()
        {
            _truckRepository?.Dispose();
        }

    }
}
