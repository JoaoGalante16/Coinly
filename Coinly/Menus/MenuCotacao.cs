using Coinly.Services;
using Coinly.Utilities;
using System.Text.RegularExpressions;
using Coinly.Clients;

namespace Coinly.Menus;

public class MenuCotacao : Menu
{
    
    public override async Task Executar()
    {
        var client = new CoinlyHttpClientFactory().CreateClient();
        await base.Executar();
        ExibirMensagemDeBusca();
        var matches = await LeitorEntrada.LerEValidarBusca();
        Console.Clear();
        if (matches is not null)
        {
            Console.WriteLine("Cotações:\n");
            foreach (Match match in matches)
            {
                var moeda = match.Groups[1].Value;
                try
                {
                    await CotacaoService.ProcessarConsulta(moeda, client);
                    ExibirMensagemVoltarAoMenu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Houve um erro\n detalhes: {ex.Message}");
                }
            }
            await new MenuPrincipal().Executar();
        }
        else
        {
            await new MenuPrincipal().Executar();
        }
    }
}
