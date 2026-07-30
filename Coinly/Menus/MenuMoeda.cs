using Coinly.Funções;
using System.Text.RegularExpressions;

namespace Coinly.Menus;

internal class MenuMoeda : Menu
{

    public override async Task Executar()
    {
        await base.Executar();
        var listaDeMoedas = await LerArquivo.LerOArquivo();
        Console.WriteLine("Digite as siglas das moedas que deseja procurar:");
        Console.WriteLine("Exemplo: BTC, USD, ETH\n");
        var entrada = Console.ReadLine().ToUpper();
        var matches = await ValidadorEntrada.ValidarEntrada(entrada);
        if (matches is not null)
        {
            await base.Executar();
            Console.WriteLine("1. Todas cotações por valor");
            Console.WriteLine("2. Todas cotações por data");
            Console.WriteLine("3. Ver resumo");
            Console.WriteLine("0. Voltar ao menu anterior\n");
            var resposta2 = int.TryParse(Console.ReadLine(), out int r) ? r : -1;
            Console.Clear();

            switch (resposta2)
            {
                case 1:
                    foreach (Match match in matches)
                    {
                        var moeda = match.Groups[1].Value;
                        Filtros.LinqFilter.FiltrarMoedaValor(listaDeMoedas, moeda);
                    }
                    Console.WriteLine("\nDigite qualquer tecla para voltar ao menu!");
                    Console.ReadKey();
                    Console.Clear();
                    await new MenuHistorico().Executar();
                    break;
                case 2:
                    foreach (Match match in matches)
                    {
                        var moeda = match.Groups[1].Value;
                        Filtros.LinqFilter.FiltrarMoedaData(listaDeMoedas, moeda);
                    }
                    Console.WriteLine("\nDigite qualquer tecla para voltar ao menu!");
                    Console.ReadKey();
                    Console.Clear();
                    await new MenuHistorico().Executar();
                    break;
                case 3:
                    foreach (Match match in matches)
                    {
                        var moeda = match.Groups[1].Value;
                        Filtros.LinqFilter.FiltrarValores(listaDeMoedas, moeda);
                    }
                    Console.WriteLine("\nDigite qualquer tecla para voltar ao menu!");
                    Console.ReadKey();
                    Console.Clear();
                    await new MenuHistorico().Executar();
                    break;
                case 0:
                    Console.Clear();
                    await new MenuHistorico().Executar();
                    break;
                default:
                    Console.WriteLine("Entrada inválida! Tente Novamente!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    await Executar();
                    break;
            }
        }
        else
        {
            await new MenuHistorico().Executar();
        }
    }
}

