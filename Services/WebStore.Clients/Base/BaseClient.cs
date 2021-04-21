using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected string Address { get; set; }

        protected HttpClient Http { get; set; }

        public BaseClient(IConfiguration configuration, string serviceAddress)
        {
            Address = serviceAddress;
            Http = new HttpClient
            {
                BaseAddress = new Uri(configuration["WebApiURL"]),
                DefaultRequestHeaders =
                {
                    Accept={new MediaTypeWithQualityHeaderValue("application/json")}
                }
            };
        }
    }
}
