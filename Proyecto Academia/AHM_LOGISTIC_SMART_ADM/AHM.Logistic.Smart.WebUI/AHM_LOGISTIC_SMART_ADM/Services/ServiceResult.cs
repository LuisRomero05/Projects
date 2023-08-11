using AHM_LOGISTIC_SMART_ADM.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Services
{
    /// <summary>
    /// Represents an operation result made in the business logic layer.
    /// </summary>
    public class ServiceResult
    {
        public int Id { get; set; }

        public bool Success { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }

        public ServiceResultType Type { get; set; }

        public ServiceResult Ok(object data = null)
        {
            Success = true;
            Data = data;
            return SetMessage("Operacíón completada exitosamente.", ServiceResultType.Success);
        }

        public ServiceResult Ok(string message)
        {
            Success = true;
            return SetMessage(message, ServiceResultType.Success);
        }

        public ServiceResult Ok(string message, object data)
        {
            Success = true;
            Data = data;
            return SetMessage(message, ServiceResultType.Success);
        }

        public ServiceResult Info(string message, bool success = true)
        {
            Success = success;
            return SetMessage(message, ServiceResultType.Info);
        }

        public ServiceResult Warning(string message, bool success = true)
        {
            Success = success;
            return SetMessage(message, ServiceResultType.Warning);
        }

        public ServiceResult Error()
        {
            return Error("An error has occurred while processing the request. If the problem persists, please contact the system administrator.");
        }

        public ServiceResult Error(string message)
        {
            Success = false;
            return SetMessage(message, ServiceResultType.Error);
        }

        public ServiceResult GatewayTimeout()
        {
            Success = false;
            return SetMessage("Se ha perdido la conexión con el servidor", ServiceResultType.GatewayTimeout);
        }

        public ServiceResult Error(ServiceProblemMessage serviceProblem)
        {
            switch (serviceProblem)
            {
                case ServiceProblemMessage.NotFound:
                    return Error("No existe el valor, verifica que tu información sea correcta.");
                case ServiceProblemMessage.ForeignKey:
                    return Error("La acción genera conflicto por que la información suministrada es incorrecta.");
                case ServiceProblemMessage.Connection:
                    return Error("Se ha perdido la conexión con el servidor.");
                case ServiceProblemMessage.Repeat:
                    return Error("Ya existe un registro con ese nombre.");
                default:
                    return Error();
            }
        }

        public ServiceResult NotFound(string message = "Object not found.", bool success = false)
        {
            Success = success;
            return SetMessage(message, ServiceResultType.NotFound);
        }

        public ServiceResult Unauthorized(string message = "Unauthorized access to object.", bool success = false)
        {
            Success = success;
            return SetMessage(message, ServiceResultType.Unauthorized);
        }

        public ServiceResult BadRequest(string message = "Bad request.", bool success = false)
        {
            Success = success;
            return SetMessage(message, ServiceResultType.BadRequest);
        }

        public ServiceResult Disabled(string message = "Disabled resource.", bool success = false)
        {
            Success = success;
            return SetMessage(message, ServiceResultType.Disabled);
        }

        public ServiceResult FromApi(IApiResult response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Message);
                case HttpStatusCode.BadRequest:
                    return Warning(response.Message, false);
                case HttpStatusCode.Conflict:
                    return Warning(response.Message, false);
                case HttpStatusCode.Gone:
                    return Warning(response.Message, false);
                case HttpStatusCode.Continue:
                    return Info(response.Message);
                case HttpStatusCode.Unauthorized:
                    return Error(response.Message);
                case HttpStatusCode.NotFound:
                    return Warning(response.Message, false);
                case HttpStatusCode.Forbidden:
                    return Error(response.Message);
                case HttpStatusCode.GatewayTimeout:
                    return GatewayTimeout();
                default:
                case HttpStatusCode.InternalServerError:
                    throw new ApiException(response);
            }
        }

        public ServiceResult SetMessage(string message, ServiceResultType serviceResultType)
        {
            Message = message;
            Type = serviceResultType;
            return this;
        }

        public ServiceResult()
        {
            Success = false;
            Type = ServiceResultType.Warning;
        }
    }


    public enum ServiceResultType
    {
        Success = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
        NotFound = 4,
        Unauthorized = 5,
        BadRequest = 6,
        Disabled = 7,
        GatewayTimeout = 8
    }

    public enum ServiceProblemMessage
    {
        NotFound = 0,
        ForeignKey = 1,
        Connection = 2,
        Repeat = 3
    }

}
