using Coinly.Filtros;
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
        var resposta = int.TryParse(Console.ReadLine(), out int r) ? r : 0;

        Console.Clear();
        switch (resposta)
        {
            case 1:
                LinqFilter.FiltrarMoedaMaisCotada(listaDeMoedas);
                LinqOrder.OrdenarPorMoedas(listaDeMoedas);
                var listaJson = LinqOrder.OrdenarParaEscreverEmJson(listaDeMoedas);
                await EscreverArquivo.EscreverNoArquivoJson(listaJson);
                Console.WriteLine("Arquivo criado com as cotações");
                
                break;
            case 2:
                await MenuMoeda.ExibirMenuMoeda();
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
