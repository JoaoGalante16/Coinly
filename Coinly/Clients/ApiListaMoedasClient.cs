using Coinly.Modelos;
using Coinly.Utilities;
using System.Text.Json;

namespace Coinly.Clients;

public class ApiListaMoedasClient
{

    public static async Task<Dictionary<string, string>> CarregarMoedas()
    {
        var client = new CoinlyHttpClient().RetornaClient();
        try
        {
            string resposta = await client.GetStringAsync("https://economia.awesomeapi.com.br/json/available/uniq");
            var resultado = JsonSerializer.Deserialize<Dictionary<string, string>>(resposta);
            return resultado;
        }

        catch (HttpRequestException ex)
        {
            return await TratadorExceptionAPI.Tratar<Dictionary<string, string>>(ex);
        }

        catch (JsonException ex)
        {
            return await TratadorExceptionAPI.Tratar<Dictionary<string, string>>(ex);
        }

        catch (Exception ex)
        {
            return await TratadorExceptionAPI.Tratar<Dictionary<string, string>>(ex);
        }
    }
}
