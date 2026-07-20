using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Filtros;

internal static class LinqFilter
{
    public static void FiltrarPorMoeda(List<Cotacao> listaDeMoedas, string moeda)
    {
        var cotacoesPorMoeda = listaDeMoedas.Where(listaDeMoedas => listaDeMoedas.Sigla.Equals(moeda)).ToList();
        Console.WriteLine($"\nCotações da {moeda}:\n");
        foreach (var m in cotacoesPorMoeda)
        {
            Console.WriteLine("--------------");
            m.MostrarCotacao();
        }
    }
}
