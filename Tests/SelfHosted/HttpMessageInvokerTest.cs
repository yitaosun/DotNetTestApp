using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace TestApp.Tests.SelfHosted
{
    class HttpMessageInvokerTest : HttpClientTest
    {
        private readonly HttpMessageInvoker m_client;

        public HttpMessageInvokerTest()
        {
            m_client = new HttpMessageInvoker(new HttpClientHandler());
        }

        // Only override because we'll have a separate instrumentation point, even though the implementation is identical
        public override void DoSomething()
        {
            ListAllProducts();
            ListProduct(1);
            ListProducts("toys");
        }

        protected override HttpResponseMessage GetAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, string.Format("http://localhost:8080/{0}", uri));
            var response = m_client.SendAsync(request, CancellationToken.None).Result;
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
