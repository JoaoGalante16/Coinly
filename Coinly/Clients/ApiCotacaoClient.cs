using Coinly.Modelos;
using Coinly.Utilities;
using System.Text.Json;

namespace Coinly.Clients;

public class ApiCotacaoClient
{
    public static async Task<Cotacao> ApiCotar(string moeda, HttpClient client)
    {
        try
        {
            string resposta = await client.GetStringAsync($"https://economia.awesomeapi.com.br/json/last/{moeda}-BRL");
            var resultado = JsonSerializer.Deserialize<Dictionary<string, Cotacao>>(resposta);
            Cotacao cotacao = resultado.Values.First();
            return cotacao;

        }

        catch (HttpRequestException ex)
        {
            return await TratadorExceptionAPI.Tratar<Cotacao>(ex);
        }

        catch (JsonException ex)
        {
            return await TratadorExceptionAPI.Tratar<Cotacao>(ex);
        }

        catch (Exception ex)
        {
            return await TratadorExceptionAPI.Tratar<Cotacao>(ex);
        }
    }
}
