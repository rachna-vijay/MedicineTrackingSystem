using MedicineTrackingSystem.Domain.Constants;

namespace MedicineTrackingSystem.Domain
{
    public class ServiceResponse
    {
        private ServiceResponse()
        {
        }
        public int StatusCode { get; private set; }
        public object Data { get; private set; }
        public string ErrorMessage { get; private set; }

        public bool IsError { get; private set; }

        /// <summary>
        /// create success
        /// </summary>
        /// <param name="result"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static ServiceResponse Success<TResult>(TResult result, int statusCode)
        {
            return new ServiceResponse { StatusCode = statusCode, Data = result };
        }

        /// <summary>
        /// create failure
        /// </summary>
        /// <param name="error"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static ServiceResponse Failure(string error, int statusCode)
        {
            return new ServiceResponse { StatusCode = statusCode, ErrorMessage = error, IsError = true };
        }
    }
}
