using Coinly.Filtros;
using Coinly.Funções;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Coinly.Menus;

internal class MenuMoeda
{

    public static async Task ExibirMenuMoeda()
    {
        var listaDeMoedas = await LerArquivo.LerOArquivo();
        Console.WriteLine("Digite as siglas das moedas que deseja procurar:");
        Console.WriteLine("Exemplo: BTC, USD, ETH");
        var resposta = Console.ReadLine().ToUpper();
        var regexValidacao = new Regex(@"^([A-Z]{3,4}),?\s?([A-Z]{3,4},?\s?)*?$");
        if (regexValidacao.IsMatch(resposta))
        {
            var regex = new Regex(@",?\s?([A-Z]{3,4})");
            var matches = regex.Matches(resposta);
            if (matches is not null)
            {
                Console.WriteLine($"1. Todas cotações por valor");
                Console.WriteLine($"2. Todas cotações por data");
                Console.WriteLine($"3. Ver resumo");
                var resposta2 = int.TryParse(Console.ReadLine(), out int r) ? r : 0;

                switch (resposta2)
                {
                    case 1:
                        foreach (Match match in matches)
                        {
                            var moeda = match.Groups[1].Value;
                            Filtros.LinqFilter.FiltrarMoedaValor(listaDeMoedas, moeda);
                        }
                        break;
                    case 2:
                        foreach (Match match in matches)
                        {
                            var moeda = match.Groups[1].Value;
                            Filtros.LinqFilter.FiltrarMoedaData(listaDeMoedas, moeda);
                        }
                        break;
                    case 3:
                        foreach (Match match in matches)
                        {
                            var moeda = match.Groups[1].Value;
                            Filtros.LinqFilter.FiltrarValores(listaDeMoedas, moeda);
                        }
                        break;
                    default:
                        Console.WriteLine("Opcção invalida");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Entrada invalida, tente novamente");
            Thread.Sleep(5000);
            Console.Clear();
            await ExibirMenuMoeda();
        }
    }
}
