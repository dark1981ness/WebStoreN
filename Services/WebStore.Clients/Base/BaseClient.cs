using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient:IDisposable
    {
        protected string Address { get; }

        protected HttpClient Http { get; }

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


        protected T Get<T>(string url) => GetAsync<T>(url).Result; //.GetAwaiter().GetResult();

        protected async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            var response = await Http.GetAsync(url, cancellationToken);
            return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>(cancellationToken);
        }

        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken cancellationToken = default)
        {
            var response = await Http.PostAsJsonAsync(url, item, cancellationToken);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync(url, item).Result;

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken cancellationToken = default)
        {
            var response = await Http.PutAsJsonAsync(url, item, cancellationToken);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancellationToken = default)
        {
            var response = await Http.DeleteAsync(url);
            return response;
        }

        public void Dispose() => Dispose(true);

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                //Очистка управляемых ресурсов
                Http.Dispose();
            }

            //Очистка неуправляемых ресурсов

            _disposed = true;
        }
    }
}
