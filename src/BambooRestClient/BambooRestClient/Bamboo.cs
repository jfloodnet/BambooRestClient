using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BambooRestClient
{
    public class Bamboo
    {
        private readonly Uri root;
        private readonly HttpClient client;

        public Bamboo(string rootUrl)
        {
            this.root = new Uri(rootUrl);
            this.client = HttpClientFactory.Create(
                new WebRequestHandler
                {
                    CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheOnly)
                },
                new RequestSniffer()
                );

            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Resource[] GetAll()
        {
            var response = client.GetStringAsync(root);
            response.Wait();

            dynamic result = JsonConvert.DeserializeObject<JObject>(response.Result);

            return JsonConvert.DeserializeObject<Resource[]>(result.resources.resource.ToString());
        }

        public Resource GetResource(string name)
        {
            return ExpectOne(GetAll().Where(x => x.Name.Equals(name)),
                "No resource found for name: " + name,
                "Multiple resources found for name:" + name);
        }

        public Result[] GetAllResults()
        {
            return GetList<Result>(
                    GetResource("result").Link,
                    response => response.results.result.ToString());
        }

        public Result GetLatestResultForPlan(string planKey)
        {
            return ExpectOne(GetAllResults().Where(result => result.IsFor(planKey)),
                "No results found for plan key: " + planKey,
                "Multiple results found for plan key: " + planKey);
        }

        public Plan[] GetAllPlans()
        {
            return GetList<Plan>(
                    GetResource("plan").Link,
                    response => response.plans.plan.ToString());
        }

        public Plan GetPlan(string key)
        {
            var planLink = ExpectOne(GetAllPlans().Where(x => x.Key.Equals(key)),

                "No plans found for key: " + key,
                "Multiple plans found for plan key: " + key).Link;

            return GetOne<Plan>(planLink);
        }

        private T[] GetList<T>(Link link, Func<dynamic, string> fieldAccessor)
        {
            var response = client.GetStringAsync(link.Href);
            response.Wait();

            dynamic result = JsonConvert.DeserializeObject<JObject>(response.Result);

            return JsonConvert.DeserializeObject<T[]>(fieldAccessor(result));
        }

        private T GetOne<T>(Link link)
        {
            var response = client.GetStringAsync(link.Href);
            response.Wait();

            dynamic result = JsonConvert.DeserializeObject<JObject>(response.Result);

            return JsonConvert.DeserializeObject<T>(result.ToString());
        }

        public T ExpectOne<T>(IEnumerable<T> array, string zeroMessage, string multipleMessage)
        {
            if (!array.Any())
            {
                throw new Exception(zeroMessage);
            }
            if (array.Count() > 1)
            {
                throw new Exception(multipleMessage);
            }

            return array.Single();
        }
    }

    public class RequestSniffer : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.RequestUri);
            return base.SendAsync(request, cancellationToken);
        }
    }
}