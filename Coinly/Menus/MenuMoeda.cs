using Coinly.Filtros;
using Coinly.Utilities;
using System.Text.RegularExpressions;

namespace Coinly.Menus;

public class MenuMoeda : Menu
{

    public override async Task Executar()
    {
        try
        {
            var listaDeMoedas = await LeitorArquivo.LerOArquivo();
            await base.Executar();
            ExibirMensagemDeBusca();
            var matches = await LeitorEntrada.LerEValidarBusca();
            if (matches is not null)
            {
                await base.Executar();
                Console.WriteLine("1. Todas cotações por valor");
                Console.WriteLine("2. Todas cotações por data");
                Console.WriteLine("3. Ver resumo");
                Console.WriteLine("0. Voltar ao menu anterior\n");
                var resposta = LeitorEntrada.LerOpcaoNumerica();
                if (listaDeMoedas is null)
                {
                    Console.WriteLine("Lista nula");
                    await Task.Delay(2000);
                    resposta = 0;
                }
                Console.Clear();

                switch (resposta)
                {
                    case 1:
                        foreach (Match match in matches)
                        {
                            var moeda = match.Groups[1].Value;
                            LinqFilter.FiltrarMoedaValor(listaDeMoedas, moeda);
                        }
                        ExibirMensagemVoltarAoMenu();
                        await new MenuHistorico().Executar();
                        break;
                    case 2:
                        foreach (Match match in matches)
                        {
                            var moeda = match.Groups[1].Value;
                            LinqFilter.FiltrarMoedaData(listaDeMoedas, moeda);
                        }
                        ExibirMensagemVoltarAoMenu();
                        await new MenuHistorico().Executar();
                        break;
                    case 3:
                        foreach (Match match in matches)
                        {
                            var moeda = match.Groups[1].Value;
                            LinqFilter.FiltrarValores(listaDeMoedas, moeda);
                        }
                        ExibirMensagemVoltarAoMenu();
                        await new MenuHistorico().Executar();
                        break;
                    case 0:
                        Console.Clear();
                        await new MenuHistorico().Executar();
                        break;
                    default:
                        await ExibirMensagemEntradaInvalida();
                        break;
                }
            }
            else
            {
                await new MenuHistorico().Executar();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Houve um erro, {ex.Message}");
        }

    }
}

