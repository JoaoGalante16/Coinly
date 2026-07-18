using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Coinly.Funções;

internal class ValidadorMoeda 
{
    private Dictionary<string, string> moedasDisponiveis = new();

    public async Task CarregarMoedas()
    {
        using (HttpClient client =  new HttpClient())
            try
            {
                string resposta = await client.GetStringAsync("https://economia.awesomeapi.com.br/json/available/uniq");
                var resultado = JsonSerializer.Deserialize<Dictionary<string,string>>(resposta);
                moedasDisponiveis = resultado;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
    }

    public async Task ValidarMoeda(string moeda)
    {
        if (moedasDisponiveis.ContainsKey(moeda))
        {
            var cotacao = await ChamadaCotacao.ApiCotar(moeda);
            cotacao.MostrarCotacao();
            await Arquivo.EscreverNoArquivo(cotacao);
            
        }
        else
        {
          Console.WriteLine($"Sigla não disponível");
        }
    }
}
