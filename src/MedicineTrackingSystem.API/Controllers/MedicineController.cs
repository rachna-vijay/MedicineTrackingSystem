using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MedicineTrackingSystem.Domain.Contracts;
using MedicineTrackingSystem.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using MedicineTrackingSystem.API.Models.Request;
using AutoMapper;
using MedicineTrackingSystem.API.Models.Response;
using System.Linq;

namespace MedicineTrackingSystem.API.Controllers
{
    /// <summary>
    /// provides an application programming interface to handle medicines
    /// </summary>
    /// <remarks>
    /// contains various methods to manage medicines
    /// </remarks>
    [Route("medicinetrackingsystem/v1/medicines")]
    [ApiController]
    public class MedicineController : BaseController
    {
        /// <summary>
        /// property to access medicine service methods
        /// </summary>
        private readonly IMedicineService _medicineService;

        private readonly IMapper _mapper;

        /// <summary>
        /// to create object of medicine controller with all its dependancies
        /// </summary>
        /// <param name="medicineService">medicine service</param>
        /// <param name="logger">logger</param>
        public MedicineController(IMedicineService medicineService, ILogger<BaseController> logger, IMapper mapper) : base(logger)
        {
            _medicineService = medicineService;
            _mapper = mapper;
        }

        /// <summary>
        /// gets all medicines
        /// </summary>
        /// <remarks>retrieves a list of all available medicines</remarks>
        /// <response code="200">Ok. Request successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>list of medicines</returns>
        [HttpGet]
        [SwaggerResponse(statusCode: 200, type: typeof(IEnumerable<MedicineListResponse>), description: "Ok. Request successful")]
        [SwaggerResponse(statusCode: 401, description: "Unauthorized")]
        [SwaggerResponse(statusCode: 500, description: "Internal Server Error")]
        public async Task<IActionResult> GetAllMedicines()
        {
            try
            {
                var result = await _medicineService.GetAllMedicines();

                var medicines = result.Select(x => new MedicineListResponse()
                {
                    Name = x.Name,
                    Brand = x.Brand,
                    ExpiryDate = x.ExpiryDate,
                    MedicineGuid = x.MedicineGuid,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList(); 

                return Ok(medicines);
            }
            catch (Exception ex)
            {
                string logMessage = $"Error Occured in {ControllerContext.ActionDescriptor.ActionName}. Error: {ex.Message}";
                _logger.LogError(logMessage);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// get medicine by id
        /// </summary>
        /// <remarks>retrieves medicine by id</remarks>
        /// <response code="200">Ok. Request successful</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>medicine</returns>
        [Route("{medicineId}")]
        [HttpGet]
        [SwaggerResponse(statusCode: 200, type: typeof(MedicineDto), description: "Ok. Request successful")]
        [SwaggerResponse(statusCode: 401, description: "Unauthorized")]
        [SwaggerResponse(statusCode: 500, description: "Internal Server Error")]
        public async Task<IActionResult> GetMEdicineById(Guid medicineId)
        {
            try
            {
                var result = await _medicineService.GetMedicineById(medicineId);
                return this.APIResponse(result, ControllerContext.ActionDescriptor.ActionName);
            }
            catch (Exception ex)
            {
                string logMessage = $"Error Occured in {ControllerContext.ActionDescriptor.ActionName}. Error: {ex.Message}";
                _logger.LogError(logMessage);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// add a new medicine
        /// </summary>
        /// <remarks>creates new medicine in the system</remarks>
        /// <param name="medicine">medicine details</param>
        /// <response code="201">Created. Request successful</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>newly added medicine</returns>
        [HttpPost]
        [SwaggerResponse(statusCode: 201, type: typeof(MedicineEditDto), description: "Created. Request successful")]
        [SwaggerResponse(statusCode: 400, description: "Bad Request")]
        [SwaggerResponse(statusCode: 401, description: "Unauthorized")]
        [SwaggerResponse(statusCode: 500, description: "Internal Server Error")]
        public async Task<IActionResult> AddMedicine(MedicineEditDto medicine)
        {
            try
            {
                var result = await _medicineService.AddMedicine(medicine);
                return this.APIResponse(result, ControllerContext.ActionDescriptor.ActionName);
            }
            catch (Exception ex)
            {
                string logMessage = $"Error Occured in {ControllerContext.ActionDescriptor.ActionName}. Error: {ex.Message}";
                _logger.LogError(logMessage);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// update medicine
        /// </summary>
        /// <remarks>updates a medicine</remarks>
        /// <param name="medicine">medicine details</param>
        /// <response code="200">Ok. Request successful</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>updated medicine</returns>
        [HttpPut]
        [SwaggerResponse(statusCode: 200, type: typeof(MedicineEditDto), description: "Ok. Request successful")]
        [SwaggerResponse(statusCode: 401, description: "Unauthorized")]
        [SwaggerResponse(statusCode: 404, type: typeof(string), description: "Not Found")]
        [SwaggerResponse(statusCode: 500, description: "Internal Server Error")]
        public async Task<IActionResult> UpdateMedicine(MedicineEditDto medicine)
        {
            try
            {
                var result = await _medicineService.UpdateMedicine(medicine);
                return this.APIResponse(result, ControllerContext.ActionDescriptor.ActionName);
            }
            catch (Exception ex)
            {
                string logMessage = $"Error Occured in {ControllerContext.ActionDescriptor.ActionName}. Error: {ex.Message}";
                _logger.LogError(logMessage);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// update medicine notes
        /// </summary>
        /// <remarks>updates notes of a medicine</remarks>
        /// <param name="notesEditRequest">notes details</param>
        /// <response code="200">Ok. Request successful</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns>medicine with updated notes</returns>
        [Route("notes")]
        [HttpPut]
        [SwaggerResponse(statusCode: 200, type: typeof(MedicineEditDto), description: "Ok. Request successful")]
        [SwaggerResponse(statusCode: 401, description: "Unauthorized")]
        [SwaggerResponse(statusCode: 404, type: typeof(string), description: "Not Found")]
        [SwaggerResponse(statusCode: 500, description: "Internal Server Error")]
        public async Task<IActionResult> UpdateNotes(NotesEditRequest notesEditRequest)
        {
            try
            {
                MedicineEditDto medicine = _mapper.Map<MedicineEditDto>(notesEditRequest);
                var result = await _medicineService.UpdateMedicine(medicine);
                return this.APIResponse(result, ControllerContext.ActionDescriptor.ActionName);
            }
            catch (Exception ex)
            {
                string logMessage = $"Error Occured in {ControllerContext.ActionDescriptor.ActionName}. Error: {ex.Message}";
                _logger.LogError(logMessage);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}