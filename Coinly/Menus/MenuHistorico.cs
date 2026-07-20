using Coinly.Funções;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Menus;

internal class MenuHistorico
{
    public static async Task MostrarHistorico()
    {
        Console.WriteLine("Mostrando historico gravado no arquivo");
        var listaMoedas = await LerArquivo.LerOArquivo();
        Filtros.LinqFilter.FiltrarPorMoeda(listaMoedas, "BTC");
        Filtros.LinqOrder.OrdenarPorValor(listaMoedas);
        Filtros.LinqOrder.OrdenarPorData(listaMoedas);
    }
}
