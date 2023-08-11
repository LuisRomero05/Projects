using AHM.Logistic.Smart.Api.Extensions;
using AHM.Logistic.Smart.BusinessLogic;
using AHM.Logistic.Smart.DataAccess.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Text;

namespace AHM.Logistic.Smart.Api
{
    public class Startup
    {
        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }      
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.DataAccess(Configuration.GetConnectionString("DefaultConnection"));
            services.BusinessLogic();
            services.AddAutoMapper(x => x.AddProfile<MappingProfileExtensions>(), AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddDbContext<LOGISTIC_SMART_AHMContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerExamples();
            services.AddSwaggerGen(x =>
            {
                x.EnableAnnotations();
                x.ExampleFilters();
                x.IncludeXmlComments(XmlCommentsFilePath);
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "AHM.Logistic.Smart.Api", Version = "v1" });
                x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Authorization header. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                x.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtOptions =>
                {
                    var key = Configuration.GetValue<string>("JwtConfig:Key");
                    var keyBytes = Encoding.ASCII.GetBytes(key);
                    jwtOptions.SaveToken = true;
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AHM.Logistic.Smart.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
