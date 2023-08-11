using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Extensions
{
    public class TrimmedFormValueProvider : FormValueProvider
    {
        public TrimmedFormValueProvider(IFormCollection values)
            : base(BindingSource.Form, values, CultureInfo.InvariantCulture)
        { }

        public override ValueProviderResult GetValue(string key)
        {
            ValueProviderResult baseResult = base.GetValue(key);
            string[] trimmedValues = baseResult.Values.Select(v => v?.Trim()).ToArray();
            return new ValueProviderResult(new StringValues(trimmedValues));
        }
    }

    public class TrimmedQueryStringValueProvider : QueryStringValueProvider
    {
        public TrimmedQueryStringValueProvider(IQueryCollection values)
            : base(BindingSource.Query, values, CultureInfo.InvariantCulture)
        { }

        public override ValueProviderResult GetValue(string key)
        {
            ValueProviderResult baseResult = base.GetValue(key);
            string[] trimmedValues = baseResult.Values.Select(v => v?.Trim()).ToArray();
            return new ValueProviderResult(new StringValues(trimmedValues));
        }
    }

    public class TrimmedFormValueProviderFactory : IValueProviderFactory
    {
        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            if (context.ActionContext.HttpContext.Request.HasFormContentType)
                context.ValueProviders.Add(new TrimmedFormValueProvider(context.ActionContext.HttpContext.Request.Form));
            return Task.CompletedTask;
        }
    }

    public class TrimmedQueryStringValueProviderFactory : IValueProviderFactory
    {
        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Add(new TrimmedQueryStringValueProvider(context.ActionContext.HttpContext.Request.Query));
            return Task.CompletedTask;
        }
    }
}
