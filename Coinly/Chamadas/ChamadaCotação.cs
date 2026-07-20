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
                //string resposta = await client.GetStringAsync($"https://economia.awesomeapi.com.br/json/last/{moeda}-BRL");
                string resposta = "{\"ETHBRL\":{\"code\":\"ETH\",\"codein\":\"BRL\",\"name\":\"Ethereum/Real Brasileiro\",\"high\":\"9808.42\",\"low\":\"9493\",\"varBid\":\"190.1\",\"pctChange\":\"1.979\",\"bid\":\"9784.87\",\"ask\":\"9784.89\",\"timestamp\":\"1784570723\",\"create_date\":\"2026-07-20 15:05:23\"}}";
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
