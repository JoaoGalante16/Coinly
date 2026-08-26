using Coinly.Modelos;
using Coinly.Utilities;
using System.Text.Json;

namespace Coinly.Clients;

public class CotacaoApiClient
{
    public static async Task<Cotacao> ApiCotar(string moeda)
    {
        var client = new CoinlyHttpClient().RetornaClient();
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
            TratadorExceptionAPI.Tratar(ex);
            return null;
        }

        catch (JsonException ex)
        {
            TratadorExceptionAPI.Tratar(ex);
            return null;
        }

        catch (Exception ex)
        {
            TratadorExceptionAPI.Tratar(ex);
            return null;
        }
    }
}
