using System;
using System.Linq;
using System.Net.Cache;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BambooRestClient
{
    public class BambooClient : IDisposable
    {
        private readonly Uri root;
        private readonly HttpClient client;

        public BambooClient(string rootUrl)
        {
            Ensure.NotNullOrEmpty(rootUrl, "rootUrl");

            this.root = new Uri(rootUrl);
            this.client = HttpClientFactory.Create(
                new WebRequestHandler
                {
                    CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable)
                },
                new RequestSniffer()
                );

            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Resource[] GetAllResources()
        {
            return client.GetList<Resource>(root, result => result.resources.resource.ToString());
        }

        public Resource GetResource(string name)
        {
            Ensure.NotNullOrEmpty(name, "name");

            return Ensure.OnlyOne(

                GetAllResources().Where(x => x.Name.Equals(name)),
                "No resource found for name: " + name,
                "Multiple resources found for name:" + name);
        }

        public Result[] GetAllResults()
        {
            return client.GetList<Result>(
                GetResource("result").Link,
                response => response.results.result.ToString());
        }

        public Result GetLatestResultForPlan(string planKey)
        {
            Ensure.NotNullOrEmpty(planKey, "planKey");

            return Ensure.OnlyOne(
                GetAllResults().Where(result => result.IsFor(planKey)),
                "No results found for plan key: " + planKey,
                "Multiple results found for plan key: " + planKey);
        }

        public Plan[] GetAllPlans()
        {
            return client.GetList<Plan>(
                GetResource("plan").Link,
                response => response.plans.plan.ToString());
        }

        public Plan GetPlan(string key)
        {
            Ensure.NotNullOrEmpty(key, "key");

            var planLink = Ensure.OnlyOne(

                GetAllPlans().Where(x => x.Key.Equals(key)),

                "No plans found for key: " + key,
                "Multiple plans found for plan key: " + key).Link;

            return client.GetOne<Plan>(planLink);
        }

        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}
