using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Filtros;

internal static class LinqFilter
{
    public static void FiltrarMoedaData(List<Cotacao> listaDeMoedas, string moeda)
    {
        var cotacoesPorMoeda = listaDeMoedas.Where(listaDeMoedas => listaDeMoedas.Sigla.Equals(moeda)).ToList();
        Console.WriteLine($"\nCotações da {moeda}:\n");
        LinqOrder.OrdenarPorData(cotacoesPorMoeda);
    }

    public static void FiltrarMoedaValor(List<Cotacao> listaDeMoedas, string moeda)
    {
        var cotacoesPorMoeda = listaDeMoedas.Where(listaDeMoedas => listaDeMoedas.Sigla.Equals(moeda)).ToList();
        Console.WriteLine($"\nCotações da {moeda}:\n");
        LinqOrder.OrdenarPorValor(cotacoesPorMoeda);

    }
}
