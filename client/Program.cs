using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Polly;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();

            while(true)
            {
                var post = new { userId = 1 };

                var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryForever(retry =>
                    {
                        Console.WriteLine("Bla bla, will retry");
                        return TimeSpan.FromSeconds(retry * 100);
                    });

                Root content = null;

                policy.Execute(() =>
                {
                    var result = client.PostAsJsonAsync("https://localhost:5000/api/basket/get-basket", post).Result;
                    Console.WriteLine($"StatusCode {result.StatusCode}, ReasonPhrase {result.ReasonPhrase}");
                    content = result.Content.ReadAsAsync<Root>().Result;
                });

                foreach(var p in content.Basket.Products)
                {
                    Console.Write(p.Name);
                    Console.WriteLine();
                }

                Thread.Sleep(1000);
            }
        }
    }

    public class Root
    {
        public Basket Basket { get; set; }
    }

    public class Basket
    {
        public string UserId { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
