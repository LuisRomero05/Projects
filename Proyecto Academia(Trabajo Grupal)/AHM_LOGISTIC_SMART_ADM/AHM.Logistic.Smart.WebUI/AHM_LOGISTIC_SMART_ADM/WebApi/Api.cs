using AHM_LOGISTIC_SMART_ADM.Configuration;
using AHM_LOGISTIC_SMART_ADM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.WebApi
{
    public class Api
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppSettings _appSettings;

        public Api(HttpClient client,
            IHttpContextAccessor httpContext,
            IOptions<AppSettings> appSettings)
        {
            _client = client;
            _httpContext = httpContext;
            _appSettings = appSettings.Value;

            _client.BaseAddress = new Uri(_appSettings.ApiSettings.BaseUrl);
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_appSettings.ApiSettings.AccessToken}");
            _client.Timeout = TimeSpan.FromSeconds(_appSettings.ApiSettings.TimeoutSeconds);
        }

        public async Task<ApiResult<T>> Get<T>(Action<ApiCallConfiguration<T>> action)
        {
            var config = new ApiCallConfiguration<T>();
            var result = new ApiResult<T>();
            try
            {
                action(config);
                var response = await _client.GetAsync(config.PathWithQueryStrings);
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ApiResult<T>>(content);
                result.Path = config.Path;
                result.StatusCode = response.StatusCode;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result.Success = true;
                    result.Type = ApiResultType.Success;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = Helpers.GetMessage(ex);
                if (result.Message.Contains("No se puede establecer una conexión")||result.Message.Contains("Cannot establish a connection"))
                {
                    result.StatusCode = HttpStatusCode.GatewayTimeout;
                }
                if (result.Message.Contains("The request was canceled due to the configured HttpClient.Timeout"))
                {
                    result.StatusCode = HttpStatusCode.GatewayTimeout;
                }
            }
            return result;
        }

        public async Task<ApiResult<T>> Post<T>(Action<ApiCallConfiguration<T>> action)
        {
            var config = new ApiCallConfiguration<T>();
            var result = new ApiResult<T>();
            try
            {
                action(config);
                var response = await _client.PostAsync(config.PathWithQueryStrings, config.ContentJson);
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ApiResult<T>>(content);
                result.Path = config.Path;
                result.StatusCode = response.StatusCode;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result.Success = true;
                    result.Type = ApiResultType.Success;
                }
            }
            catch (Exception ex)
            {
                
                result.Success = false;
                result.Message = Helpers.GetMessage(ex);
                if (result.Message.Contains("No se puede establecer una conexión") || result.Message.Contains("Cannot establish a connection"))
                {
                    result.StatusCode = HttpStatusCode.GatewayTimeout;
                }
                if(result.Message.Contains("The request was canceled due to the configured HttpClient.Timeout"))
                {
                    result.StatusCode = HttpStatusCode.GatewayTimeout;
                }
               
            }
            return result;
        }

        public async Task<ApiResult> Put<T>(Action<ApiCallConfiguration<T>> action)
        {
            var config = new ApiCallConfiguration<T>();
            var result = new ApiResult();
            try
            {

                action(config);

                var response = await _client.PutAsync(config.PathWithQueryStrings, config.ContentJson);
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ApiResult>(content);
                result.Path = config.Path;
                result.StatusCode = response.StatusCode;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result.Success = true;
                    result.Type = ApiResultType.Success;
                }


            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = Helpers.GetMessage(ex);
                if (result.Message.Contains("No se puede establecer una conexión") || result.Message.Contains("Cannot establish a connection"))
                {
                    result.StatusCode = HttpStatusCode.GatewayTimeout;
                }
                if (result.Message.Contains("The request was canceled due to the configured HttpClient.Timeout"))
                {
                    result.StatusCode = HttpStatusCode.GatewayTimeout;
                }
            }

            return result;
        }

        public async Task<ApiResult> Delete<T>(Action<ApiCallConfiguration<T>> action)
        {
            var config = new ApiCallConfiguration<T>();
            var result = new ApiResult();
            try
            {
                action(config);

                var response = await _client.DeleteAsync(config.PathWithQueryStrings);
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ApiResult>(content);
                result.Path = config.Path;
                result.StatusCode = response.StatusCode;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result.Success = true;
                    result.Type = ApiResultType.Success;

                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = Helpers.GetMessage(ex);
                if (result.Message.Contains("No se puede establecer una conexión") || result.Message.Contains("Cannot establish a connection"))
                {
                    result.StatusCode = HttpStatusCode.GatewayTimeout;
                }
                if (result.Message.Contains("The request was canceled due to the configured HttpClient.Timeout"))
                {
                    result.StatusCode = HttpStatusCode.GatewayTimeout;
                }
            }

            return result;
        }
    }
}
