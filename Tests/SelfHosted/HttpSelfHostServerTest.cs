using System;
using System.Threading;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TestApp.Tests.SelfHosted
{
    class HttpSelfHostServerTest : ATest
    {
        public override void DoSomething()
        {
            var thread = new Thread(() =>
            {
                var config = new HttpSelfHostConfiguration("http://localhost:8080");
                config.Routes.MapHttpRoute("API Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
                var server = new HttpSelfHostServer(config);
                server.OpenAsync().Wait();
            });
            thread.Start();
        }
    }

    public class ProductsController : ApiController
    {
        Product[] m_products = new Product[]  
        {  
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },  
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },  
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }  
        };

        public void Sleep()
        {
            Thread.Sleep(100);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            Sleep();
            System.Threading.Thread.Sleep(100);
            return m_products;
        }

        public Product GetProductById(int id)
        {
            Sleep();
            var product = m_products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            Sleep();
            return m_products.Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }
    }
}
