using Microsoft.Extensions.Configuration;
using System;
using WebStore.Clients.Products;

namespace WebStore.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var products_client = new ProductsClient(configuration);

            foreach (var product in products_client.GetProducts())
            {
                Console.WriteLine($"{product.Name} - {product.Price}");
            }
        }
    }
}
