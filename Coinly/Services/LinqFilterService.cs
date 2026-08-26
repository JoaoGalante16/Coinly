using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Services;

internal class LinqFilterService
{
    public static void FiltrarMoedaPorParametro<TKey>(List<Cotacao> listaDeMoedas, string moeda, Func<Cotacao, TKey> chaveDeOrdenacao)
    {
        var cotacoesPorMoeda = listaDeMoedas.Where(c => c.Sigla.Equals(moeda))
                .OrderByDescending(chaveDeOrdenacao)
                .ToList();
        if (cotacoesPorMoeda.Count > 0)
        {
            Console.WriteLine($"\nCotações da {moeda}:\n");
            Cotacao.MostrarCotacaoTabela();
            foreach (var c in cotacoesPorMoeda)
            {
                c.MostrarCotacao();
            }
        }
        else Console.WriteLine($"\nNão existe cotações feita da {moeda}\n");

    }
}