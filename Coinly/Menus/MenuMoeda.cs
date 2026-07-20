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
        Console.WriteLine("Digite a sigla da moeda que deseja procurar:");
        var entrada = Console.ReadLine().ToUpper();
        Console.WriteLine($"1. Todas cotações por valor de {entrada}");
        Console.WriteLine($"2. Todas cotações por data de {entrada}");
        var resposta2 = int.Parse(Console.ReadLine());

        switch (resposta2)
            {
                case 1:
                Filtros.LinqFilter.FiltrarMoedaValor(listaDeMoedas, entrada);
                break;
                case 2:
                Filtros.LinqFilter.FiltrarMoedaData(listaDeMoedas, entrada);
                break;
                default:
                Console.WriteLine("Opcção invalida");
                break;
            }
    }
}
