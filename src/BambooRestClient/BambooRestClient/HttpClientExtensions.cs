using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BambooRestClient
{
    internal static class HttpClientExtensions
    {
        public static T[] GetList<T>(this HttpClient client, Link link, Func<dynamic, string> fieldAccessor)
        {
            var response = client.GetStringAsync(link.Href);
            response.Wait();

            dynamic result = JsonConvert.DeserializeObject<JObject>(response.Result);

            return JsonConvert.DeserializeObject<T[]>(fieldAccessor(result));
        }

        public static  T GetOne<T>(this HttpClient client, Link link)
        {
            var response = client.GetStringAsync(link.Href);
            response.Wait();

            dynamic result = JsonConvert.DeserializeObject<JObject>(response.Result);

            return JsonConvert.DeserializeObject<T>(result.ToString());
        }
    }
}