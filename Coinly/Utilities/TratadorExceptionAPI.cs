using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Coinly.Utilities
{
    internal static class TratadorExceptionAPI
    {

        public static Task<T> Tratar<T>(HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Console.WriteLine("Limite de requisições atingido. Aguarde antes de tentar novamente.");
                return Task.FromResult(default(T));
            }

            else if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("A URL da API ou o recurso solicitado não foi encontrado.");
                return Task.FromResult(default(T));
            }

            else
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return Task.FromResult(default(T));
            }
        }

        public static Task<T> Tratar<T>(JsonException ex)
        {
            Console.WriteLine("Erro de parse/formato de JSON.");
            return Task.FromResult(default(T));
        }

        public static Task<T> Tratar<T>(Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            return Task.FromResult(default(T));
        }


    }
}
