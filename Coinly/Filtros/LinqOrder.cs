using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Filtros;

internal class LinqOrder
{
    public static void OrdenarPorMoedas(List<Cotacao> ListaDeMoedas)
    {
        if (ListaDeMoedas is not null)
        {
            var ListaOrdenada = ListaDeMoedas.OrderBy(c => c.Sigla)
                .ThenByDescending(c => c.Timestamp)
                .GroupBy(c => c.Sigla)
                .ToList();


            foreach (var moeda in ListaOrdenada)
            {
                var borda = "".PadRight(50, '=');
                Console.WriteLine($"\n{borda}\n");
                Cotacao.MostrarCotacaoTabela();
                foreach (var cotacao in moeda)
                {
                    cotacao.MostrarCotacao();
                }
            }
        }
    }

    public static List<MoedaAgrupada> OrdenarParaEscreverEmJson(List<Cotacao> cotacoes)
    {
        if (cotacoes is not null)
        {
            var listaOrdenada = cotacoes
            .GroupBy(m => m.Sigla)
            .Select(c => new MoedaAgrupada { Sigla = c.Key, Cotacoes = c.OrderByDescending(m => m.Timestamp).ToList(), Total = c.Count() })
            .ToList();

            return listaOrdenada;
        }
        else return null;
    }
}
