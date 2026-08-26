using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Coinly.Utilities
{
    internal static class TratadorExceptionAPI
    {

        public static void Tratar(HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Console.WriteLine("Limite de requisições atingido. Aguarde antes de tentar novamente.");
            }

            else if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("A URL da API ou o recurso solicitado não foi encontrado.");
            }

            else
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        public static void Tratar(JsonException ex)
        {
            Console.WriteLine("Erro de parse/formato de JSON.");
        }

        public static void Tratar(Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }


    }
}
