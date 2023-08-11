using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.API.Swagger
{
    public class SwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _apiVersionDescriptor;
        public SwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptor)
        {
            _apiVersionDescriptor = apiVersionDescriptor;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVersionDescriptor.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, GetSwaggerDocInfo(description));
            }
        }
        static OpenApiInfo GetSwaggerDocInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = $"Template API {description.ApiVersion}",
                Version = description.GroupName,
                Description = "Esta API contiene los servicios para consultar Templade",
                License = new OpenApiLicense() { Name = "All rights reserved 2022" }
            };

            if (description.IsDeprecated)
            {
                info.Description += $"{description.ApiVersion} está descontinuada, favor seleccionar otra versión";
            }
            return info;
        }
    }
}
