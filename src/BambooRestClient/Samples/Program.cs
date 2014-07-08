using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BambooRestClient;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new BambooClient("http://tools-bamboo:8085/rest/api/latest/"))
            {
                var resources = client.GetAll();
                var plan = client.GetPlan("QUOTE-CI");
            }
        }
    }
}
