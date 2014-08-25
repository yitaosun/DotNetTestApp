using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace TestApp.Tests.SelfHosted
{
    class HttpClientTest : ATest
    {
        private readonly HttpClient m_client;

        public HttpClientTest()
        {
            m_client = new HttpClient();
            m_client.BaseAddress = new Uri("http://localhost:8080");
        }

        public override void DoSomething()
        {
            ListAllProducts();
            ListProduct(1);
            ListProducts("toys");
        }

        protected void ListAllProducts()
        {
            var resp = GetAsync("api/products");

            var products = resp.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            foreach (var p in products)
            {
                Console.WriteLine("{0} {1} {2} ({3})", p.Id, p.Name, p.Price, p.Category);
            }
        }

        protected void ListProduct(int id)
        {
            var resp = GetAsync(string.Format("api/products/{0}", id));

            var product = resp.Content.ReadAsAsync<Product>().Result;
            Console.WriteLine("ID {0}: {1}", id, product.Name);
        }

        protected void ListProducts(string category)
        {
            Console.WriteLine("Products in '{0}':", category);

            var resp = GetAsync(string.Format("api/products?category={0}", category));
            var products = resp.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            foreach (var product in products)
            {
                Console.WriteLine(product.Name);
            }
        }

        protected virtual HttpResponseMessage GetAsync(string uri)
        {
            var response = m_client.GetAsync(uri).Result;
            response.EnsureSuccessStatusCode();
            return response;
        }

    }
}
