using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace AHM.Logistic.Smart.BusinessLogic.Logger
{
    public class CustomJsonResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);

            if (prop.PropertyType.IsClass &&
                prop.PropertyType != typeof(string))
            {
                prop.ShouldSerialize = obj => false;
            }

            return prop;
        }
    }
}
