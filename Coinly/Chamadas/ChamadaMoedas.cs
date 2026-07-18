using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Coinly.Chamadas;

internal class ChamadaMoedas
{
    public static Dictionary<string, string> moedasDisponiveis = new();

    public static async Task<Dictionary<string, string>> CarregarMoedas()
    {
        using (HttpClient client = new HttpClient())
            try
            {
                string resposta = await client.GetStringAsync("https://economia.awesomeapi.com.br/json/available/uniq");
                var resultado = JsonSerializer.Deserialize<Dictionary<string, string>>(resposta);
                moedasDisponiveis = resultado;
                return moedasDisponiveis;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return moedasDisponiveis;
            }
    }
}
