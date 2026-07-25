using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Coinly.Chamadas;

internal class ChamadaCotacao
{
    public static async Task<Cotacao> ApiCotar(string moeda)
    {
        using (HttpClient client = new HttpClient())
            try
            {
                string resposta = await client.GetStringAsync($"https://economia.awesomeapi.com.br/json/last/{moeda}-BRL");
                //string resposta = "{\"ETHBRL\":{\"code\":\"USD\",\"codein\":\"BRL\",\"name\":\"Ethereum/Real Brasileiro\",\"high\":\"9808.42\",\"low\":\"9493\",\"varBid\":\"190.1\",\"pctChange\":\"1.979\",\"bid\":\"5144.87\",\"ask\":\"5.89\",\"timestamp\":\"178457070\",\"create_date\":\"2026-07-20 15:05:23\"}}";
                var resultado = JsonSerializer.Deserialize<Dictionary<string, Cotacao>>(resposta);
                Cotacao cotacao = resultado.Values.First();
                return cotacao;

            }

            catch (HttpRequestException ex)
            {
                if(ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    Console.WriteLine("Limite de requisições atingido. Aguarde antes de tentar novamente.");
                    return null;
                }
                else if(ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("A URL da API ou o recurso solicitado não foi encontrado.");
                    return null;
                }else
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    return null;
                }
            
            }

            catch (JsonException ex)
            {
                Console.WriteLine("Erro de parse/formato de JSON.");
                return null;
            }

            catch (Exception ex)
            {
               Console.WriteLine($"Erro: {ex.Message}");
               return null;
            }
    }
}
