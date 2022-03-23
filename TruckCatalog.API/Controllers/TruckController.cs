using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckCatalog.App.Aplication.Commands;
using TruckCatalog.App.Aplication.Query;
using TruckCatalog.App.Aplication.ViewModels;
using TruckCatalog.App.Core.ApiServices.Controllers;
using TruckCatalog.App.Core.Mediator;
using TruckCatalog.App.Models;

namespace TruckCatalog.API.Controllers
{
    [Route("api/truck")]
    public class TruckController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        private readonly ITruckQuery _truckQuery;

        public TruckController(IMediatorHandler mediatorHandler, ITruckQuery truckQuery)
        {
            _mediatorHandler = mediatorHandler;
            _truckQuery = truckQuery;
        }


        /// <summary>
        /// Returns all Trucks
        /// </summary>        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TruckViewModel>>> GetAll()
        {
            var response = await _truckQuery.GetAll();
            if (response.Count() == 0)
                return CustomResponse("No trucks found.");

            return response.Select(TruckViewModel.Mapear).ToList();
        }

        /// <summary>
        /// Returns a truck by id
        /// </summary>
        /// <param name="id">Truck Guid</param>        
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<TruckViewModel>> GetById(Guid id)
        {
            var truck = await _truckQuery.GetById(id);
            if (truck == null)
                return CustomResponse("No trucks found.");

            return TruckViewModel.Mapear(truck);
        }

        /// <summary>
        /// Register a truck
        /// </summary>
        /// <param name="viewModel">
        /// Model: Truck Model Enum (FH = 1, FM = 2);           
        /// ModelYear: Truck model year, must be current or next year;   
        /// </param>        
        [HttpPost]
        public async Task<ActionResult> Post(AddTruckViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var comando = new AddTruckCommand
                (viewModel.Model, viewModel.ModelYear);

            return CustomResponse(await _mediatorHandler.SendCommand(comando));
        }

        /// <summary>
        /// Update a truck
        /// </summary>
        /// <param name="viewModel">
        /// Id: Truck Guid;    
        /// Model: Truck Model Enum (FH = 1, FM = 2);           
        /// ModelYear: Truck model year, must be current or next year;   
        /// </param>        
        [HttpPut]
        public async Task<ActionResult> Put(UpdateTruckViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var comando = new UpdateTruckCommand
                (viewModel.Id, viewModel.Model, viewModel.ModelYear);

            return CustomResponse(await _mediatorHandler.SendCommand(comando));
        }

        /// <summary>
        /// Remove a truck
        /// </summary>
        /// <param name="id">Truck Guid</param>        
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var comando = new DeleteTruckCommand(id);

            return CustomResponse(await _mediatorHandler.SendCommand(comando));
        }
    }
}
