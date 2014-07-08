using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BambooRestClient
{
    public class RequestSniffer : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.RequestUri);
            return base.SendAsync(request, cancellationToken);
        }
    }
}