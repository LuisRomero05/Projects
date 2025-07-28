using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Template.BusinessLogic;

namespace Template.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TokenController : ApiBaseController
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("gererate")]
        [AllowAnonymous]
        public IActionResult GenerarToken()
        {
            var serviceResult = new ServiceResult();
            try
            {
                string key = _configuration.GetValue<string>("AppSettings:JwtSecretKey");
                var bytesKey = Encoding.ASCII.GetBytes(key);
                DateTime validez = DateTime.Now;

                var handler = new JwtSecurityTokenHandler();
                var descriptor = new SecurityTokenDescriptor()
                {
                    NotBefore = validez,
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]{
                        new Claim("dt", validez.ToString("yyyyy-MM-ddTHH:mm:ssK"))
                    }),
                    Expires = validez.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytesKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = handler.CreateToken(descriptor);
                var finalToken = handler.WriteToken(token);
                serviceResult.Success = true;
                serviceResult.Type = ServiceResultType.Success;
                serviceResult.Message = "Token generado correctamente";
                serviceResult.Data = finalToken;
            }
            catch (Exception ex)
            {
                serviceResult.Success = false;
                serviceResult.Type = ServiceResultType.BadRequest;
                serviceResult.Message = ex.Message;
            }
            return ApiServiceResult(serviceResult);
        }
    }
}
