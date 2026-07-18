using Coinly.Chamadas;
using Coinly.Funções;
using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Coinly.Services;

internal class CotacaoService 
{
    public async Task ProcessarConsulta(string moeda)
    {
        var moedas = await ChamadaMoedas.CarregarMoedas();
        if (moedas.ContainsKey(moeda))
        {
           
            var cotacao = await ChamadaCotacao.ApiCotar(moeda);
            cotacao.MostrarCotacao();
            await EscreverArquivo.EscreverNoArquivo(cotacao);
        }
        else
        {
          Console.WriteLine($"Sigla não disponível");
        }
    }
}
