using DotLms.Services.Providers.Contracts;
using Newtonsoft.Json;

namespace DotLms.Services.Providers
{
    public class JsonConvertProvider<T> : IJsonConvertProvider<T> where T : class
    {
        public string SerializeObject(object value)
        {
            var config = new JsonSerializerSettings();
            config.ReferenceLoopHandling= ReferenceLoopHandling.Ignore;
           // config.PreserveReferencesHandling= PreserveReferencesHandling.Objects;
            return JsonConvert.SerializeObject(value, config);
        }

        public T DeserializeObect(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}