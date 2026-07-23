using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Filtros;

internal static class LinqFilter
{
    public static void FiltrarMoedaData(List<Cotacao> listaDeMoedas, string[] moeda)
    {
        var i = 0;
        foreach(var m in moeda)
        {
            var cotacoesPorMoeda = listaDeMoedas.Where(c => c.Sigla.Equals(moeda[i]))
                    .OrderByDescending(lista => lista.Timestamp)
                    .ToList();
            if (cotacoesPorMoeda.Count > 0)
            {
                Console.WriteLine($"\nCotações da {moeda[i]}:\n");
                Cotacao.MostrarCotacaoTabela();
                foreach (var c in cotacoesPorMoeda)
                {
                    c.MostrarCotacao();
                }
            }
            else Console.WriteLine($"\nNão existe cotações feita da {moeda[i]}\n");
            i++;
        }
    }

    public static void FiltrarMoedaValor(List<Cotacao> listaDeMoedas, string[] moeda)
    {
        var i = 0;
        foreach (var m in moeda)
        {
            var cotacoesPorMoeda = listaDeMoedas.Where(listaDeMoedas => listaDeMoedas.Sigla.Equals(moeda[i]))
                    .OrderByDescending(lista => lista.Valor)
                    .ToList();
            if (cotacoesPorMoeda.Count > 0)
            {
                Console.WriteLine($"\nCotações da {moeda[i]}:\n");
                Cotacao.MostrarCotacaoTabela();
                foreach (var c in cotacoesPorMoeda)
                {
                    c.MostrarCotacao();
                }
            }
            else Console.WriteLine($"\nNão existe cotações feita da {moeda[i]}\n");
            i++;
        } 
    }

    public static void FiltrarValores(List<Cotacao> listaDeMoedas, string[] moeda)
    {
        var i = 0;
        foreach (var m in moeda)
        {
            var moedaespecifica = listaDeMoedas.Where(lista => lista.Sigla.Equals(moeda[i])).OrderByDescending(c => c.Timestamp).ToList();
            if (moedaespecifica.Count > 0)
            {
                Console.WriteLine($"\nResumo {moeda[i]}\n");
                var variacao = moedaespecifica.First().Valor - moedaespecifica.Last().Valor;
                Console.WriteLine($"Maior cotação já resgistrada de: {moedaespecifica.Max(c => c.Valor)}");
                Console.WriteLine($"Menor cotação já resgistrada de: {moedaespecifica.Min(c => c.Valor)}");
                Console.WriteLine($"Valor Medio das cotações de: {moedaespecifica.Average(c => c.Valor)}");
                Console.WriteLine($"Variação entre primeira e ultima cotação de: {variacao}");
            }
            else Console.WriteLine($"\nNão existe cotações feita da {moeda[i]}\n");
            i++;
        }
        
    }

    public static void FiltrarMoedaMaisCotada(List<Cotacao> listaDeMoedas)
    {
        var moedaMaisCotada = listaDeMoedas.GroupBy(c => c.Sigla)
                    .Select(g => new { Sigla = g.Key, Valor = g, Total = g.Count() }).
                    MaxBy(s => s.Total);
        if (moedaMaisCotada is not null)
        {
            Console.WriteLine($"A moeda mais cotada é {moedaMaisCotada.Sigla} com {moedaMaisCotada.Total} cotações\n");
        }
    }

}
