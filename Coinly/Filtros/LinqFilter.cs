using Coinly.Modelos;
using Coinly.Services;

namespace Coinly.Filtros;

public static class LinqFilter
{
    public static void FiltrarMoedaData(List<Cotacao> listaDeMoedas, string moeda)
    {
        LinqFilterService.FiltrarMoedaPorParametro(listaDeMoedas, moeda, c => c.Timestamp);
    }

    public static void FiltrarMoedaValor(List<Cotacao> listaDeMoedas, string moeda)
    {
        LinqFilterService.FiltrarMoedaPorParametro(listaDeMoedas, moeda, c => c.Valor);
    }

    public static void FiltrarValores(List<Cotacao> listaDeMoedas, string moeda)
    {
        var moedaespecifica = listaDeMoedas.Where(lista => lista.Sigla.Equals(moeda)).OrderByDescending(c => c.Timestamp).ToList();
        if (moedaespecifica.Count > 0)
        {
            Console.WriteLine($"\nResumo {moeda}\n");
            var variacao = moedaespecifica.First().Valor - moedaespecifica.Last().Valor;
            Console.WriteLine($"Maior cotação já resgistrada: {moedaespecifica.Max(c => c.Valor)}");
            Console.WriteLine($"Menor cotação já resgistrada: {moedaespecifica.Min(c => c.Valor)}");
            Console.WriteLine($"Valor Medio das cotações: {moedaespecifica.Average(c => c.Valor)}");
            Console.WriteLine($"Variação entre primeira e ultima cotação: {variacao}");
        }
        else Console.WriteLine($"\nNão existe cotações feita da {moeda}\n");

    }

    public static void FiltrarMoedaMaisCotada(List<Cotacao> listaDeMoedas)
    {
        var moedaMaisCotada = listaDeMoedas.GroupBy(c => c.Sigla)
                    .Select(g => new { Sigla = g.Key, Valor = g, Total = g.Count() }).
                    MaxBy(s => s.Total);
        if (moedaMaisCotada is not null)
        {
            Console.WriteLine($"A moeda mais cotada é {moedaMaisCotada.Sigla} com {moedaMaisCotada.Total} cotações");
        }
    }

}
