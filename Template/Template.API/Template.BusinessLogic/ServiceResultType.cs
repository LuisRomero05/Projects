using System;
using System.Collections.Generic;
using System.Text;

namespace Template.BusinessLogic
{
    public enum ServiceResultType
    {
        info = 100,
        Success = 200,
        Warning = 202,
        BadRecuest = 400,
        Unauthorize = 401,
        Forbidden = 203,
        NotFound = 404,
        NotAcceptable = 406,
        Conflict = 409,
        Disabled = 410,
        Error = 500,
        BadRequest = 501,
        Unauthorized = 502,
        Info = 503
    }
}
