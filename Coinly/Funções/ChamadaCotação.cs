using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Coinly.Funções;

internal class ChamadaCotacao
{
    public async Task Cotar(string moeda)
    {
        using (HttpClient client = new HttpClient())
            try
            {
                string resposta = await client.GetStringAsync($"https://economia.awesomeapi.com.br/json/last/{moeda}-BRL");
                var resultado = JsonSerializer.Deserialize<Dictionary<string, Cotacao>>(resposta);
                Cotacao cotacao = resultado.Values.First();
                cotacao.MostrarCotacao();

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
    }
}
