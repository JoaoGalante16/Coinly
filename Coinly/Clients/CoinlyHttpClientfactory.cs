using System;
using System.Net.Http;

namespace Coinly.Clients
{
    public class CoinlyHttpClientFactory : IHttpClientFactory
    {
        private static readonly HttpClient _client = new HttpClient();

        public HttpClient CreateClient(string name)
        {
            return _client;
        }
    }
}