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
                .ThenBy(c => c.Timestamp)
                .GroupBy(c => c.Sigla);
            

            foreach (var moeda in ListaOrdenada)
            {
                Console.WriteLine("\n===============================\n");
                foreach (var cotacao in moeda)
                {
                    cotacao.MostrarCotacao();
                }
            }
        }
    }
}
