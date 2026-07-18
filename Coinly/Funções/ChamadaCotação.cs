using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Coinly.Funções;

internal class ChamadaCotacao
{
    public static async Task<Cotacao> ApiCotar(string moeda)
    {
        using (HttpClient client = new HttpClient())
            try
            {
                string resposta = await client.GetStringAsync($"https://economia.awesomeapi.com.br/json/last/{moeda}-BRL");
                var resultado = JsonSerializer.Deserialize<Dictionary<string, Cotacao>>(resposta);
                Cotacao cotacao = resultado.Values.First();
                return cotacao;

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               return new Cotacao() ;
            }
    }
}
