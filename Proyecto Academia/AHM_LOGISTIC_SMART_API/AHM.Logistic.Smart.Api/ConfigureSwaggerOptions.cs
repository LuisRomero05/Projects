using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {

        readonly IApiVersionDescriptionProvider _apiVerProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVerProvider) => _apiVerProvider = apiVerProvider;
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVerProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, GetSwaggerDocInfo(description));
            }
        }


        static OpenApiInfo GetSwaggerDocInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = $"File Log API {description.ApiVersion}",
                Version = description.GroupName,
                Description = "Use this API to query file logs.",
                Contact = new OpenApiContact()
                {
                    Name = "Web API Query File Logs"
                },
                License = new OpenApiLicense()
                {
                    Name = "BCIE, All rights reserved (2020)."
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += $" {description.ApiVersion} API version is deprecated.";
            }

            return info;
        }
    }
}
