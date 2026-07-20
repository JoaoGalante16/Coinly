using Coinly.Funções;
using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Menus;

internal class MenuHistorico
{
    public static async Task MostrarHistorico()
    {
        var listaDeMoedas = await LerArquivo.LerOArquivo();
        Console.WriteLine("1. Mostrar todas cotações");
        Console.WriteLine("2. Pesquisar uma moeda");
        var resposta = int.Parse(Console.ReadLine());

        Console.Clear();
        switch (resposta)
        {
            case 1:
                foreach(var m in listaDeMoedas)
                {
                    m.MostrarCotacao();
                }
                break;
            case 2:
                MenuMoeda.EixibirMenuMoeda();
                break;
            default:
                Console.WriteLine("Opção invalida");
                break;
        }

        //Console.WriteLine("Mostrando historico gravado no arquivo");
        //var listaMoedas = await LerArquivo.LerOArquivo();
        //Filtros.LinqFilter.FiltrarPorMoeda(listaMoedas, "BTC");
        //Filtros.LinqOrder.OrdenarPorValor(listaMoedas);
        //Filtros.LinqOrder.OrdenarPorData(listaMoedas);
    }
}
