using Coinly.Filtros;
using Coinly.Funções;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Menus;

internal class MenuMoeda
{
    
    public static async Task EixibirMenuMoeda() 
    {
        var listaDeMoedas = await LerArquivo.LerOArquivo();
        Console.WriteLine("Digite as siglas das moedas que deseja procurar:");
        Console.WriteLine("Exemplo: BTC, USD, ETH");
        var entrada = Console.ReadLine().ToUpper().Split(',').Select(s => s.Trim()).ToArray();
        Console.WriteLine($"1. Todas cotações por valor");
        Console.WriteLine($"2. Todas cotações por data");
        Console.WriteLine($"3. Ver resumo");
        var resposta2 = int.Parse(Console.ReadLine());

        switch (resposta2)
            {
                case 1:
                Filtros.LinqFilter.FiltrarMoedaValor(listaDeMoedas, entrada);
                break;
                case 2:
                Filtros.LinqFilter.FiltrarMoedaData(listaDeMoedas, entrada);
                break;
                case 3:
                Filtros.LinqFilter.FiltrarValores(listaDeMoedas, entrada);
                break;
                default:
                Console.WriteLine("Opcção invalida");
                break;
            }
    }
}
