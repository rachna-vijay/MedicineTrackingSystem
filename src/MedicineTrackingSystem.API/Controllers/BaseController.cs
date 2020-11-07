using MedicineTrackingSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MedicineTrackingSystem.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// creates API Response 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected IActionResult APIResponse(ServiceResponse result, string action)
        {
            if (result.IsError)
            {
                string logMessage = $"Error occured at: {action}. Error Message: {result.ErrorMessage}. Status Code: {result.StatusCode}";
                _logger.LogError(logMessage);
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }

            _logger.LogInformation($"Action {action} called Successfully.Status Code: {result.StatusCode}. Data: {result.Data}");

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
