using Coinly.Filtros;
using Coinly.Utilities;

namespace Coinly.Menus;

public class MenuHistorico : Menu
{
    public override async Task Executar()
    {
        await base.Executar();
        try
        {
            var listaDeMoedas = await LeitorArquivo.LerOArquivo();
            Console.WriteLine("1. Mostrar todas cotações");
            Console.WriteLine("2. Pesquisar uma moeda");
            Console.WriteLine("0. Voltar ao menu principal\n");
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
                    LinqFilter.FiltrarMoedaMaisCotada(listaDeMoedas);
                    LinqOrder.OrdenarPorMoedas(listaDeMoedas);
                    var listaJson = LinqOrder.OrdenarParaEscreverEmJson(listaDeMoedas);
                    try
                    {
                        var arquivoJson = await EscritorArquivo.EscreverNoArquivoJson(listaJson);
                        Console.WriteLine(arquivoJson);
                        ExibirMensagemVoltarAoMenu();
                        await Executar();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"houve um erro ao escrever no arquivo, {ex.Message}");
                    }
                    break;
                case 2:
                    await new MenuMoeda().Executar();
                    break;
                case 0:
                    Console.Clear();
                    await new MenuPrincipal().Executar();
                    break;
                default:
                    await ExibirMensagemEntradaInvalida();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Houve um erro, {ex.Message}");
        }
        
    }
}
