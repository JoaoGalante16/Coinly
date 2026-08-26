using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Clients
{
    internal class CoinlyHttpClient
    {

        private static HttpClient client = new HttpClient();

        public HttpClient RetornaClient()
        {
            return client;
        }
    }
}
