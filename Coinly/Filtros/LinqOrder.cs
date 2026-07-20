using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Filtros;

internal class LinqOrder
{
    public static void OrdenarPorValor (List<Cotacao> lista)
    {
        var ordernadoPorValor = lista.OrderByDescending(lista => lista.Valor).ToList();
        Console.WriteLine("\nOrdenando por valor:\n");
        foreach (var c in ordernadoPorValor)
        { 
            c.MostrarCotacao();
        }
    }

    public static void OrdenarPorData (List<Cotacao> lista)
    {
        var ordenarPorTimestamp = lista.OrderByDescending(lista => lista.Timestamp).ToList();
        Console.WriteLine("\nOrdenando por mais recente:\n");
        foreach(var c in ordenarPorTimestamp)
        {
            c.MostrarCotacao();
        }
    }
}
