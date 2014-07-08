using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BambooRestClient;
using BambooRestClient.Resources;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var planKey = ConfigurationManager.AppSettings["PlanKey"] ?? "QUOTE-CI";

            using (var client = new BambooClient("http://tools-bamboo:8085/rest/api/latest/"))
            {
                var result = client.GetLatestResultForPlan(planKey);
                
                if (!result.WasSuccessful())
                {
                    OpenBrowserAtBuildResult(result);
                }
            }
        }

        private static void OpenBrowserAtBuildResult(Result result)
        {
            var url = string.Format("http://tools-bamboo:8085/browse/{0}/latest", result.Plan.Key);
            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", url);
        }
    }
}
