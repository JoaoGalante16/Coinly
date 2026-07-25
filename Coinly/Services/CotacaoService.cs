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
    public static async Task ProcessarConsulta(string moeda)
    {
        var moedas = await ChamadaMoedas.CarregarMoedas();
        if (moedas is not null)
        {
            if (moedas.ContainsKey(moeda))
            {
                var cotacao = await ChamadaCotacao.ApiCotar(moeda);
                if (cotacao is not null)
                {
                    Cotacao.MostrarCotacaoTabela();
                    cotacao.MostrarCotacao();
                    await EscreverArquivo.EscreverNoArquivo(cotacao);
                }
            }
            else
            {
                Console.WriteLine($"\n{moeda} não disponível\n");
            }
        }
    }
}
