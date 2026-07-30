using Coinly.Funções;
using Coinly.Services;
using System.Text.RegularExpressions;

namespace Coinly.Menus;

internal class MenuCotacao : Menu
{
    public override async Task Executar()
    {
        await base.Executar();
        Console.WriteLine("Digite a(s) sigla(s) da(s) moeda(s) que deseja cotar: ");
        Console.WriteLine("Exemplo: BTC, USD, ETH\n");
        var entrada = Console.ReadLine().ToUpper();
        var matches = await ValidadorEntrada.ValidarEntrada(entrada);
        Console.Clear();
        if (matches is not null)
        {
            Console.WriteLine("Cotações:\n");
            foreach (Match match in matches)
            {
                var moeda = match.Groups[1].Value;
                await CotacaoService.ProcessarConsulta(moeda);
            }
            Console.WriteLine("\nDigite qualquer tecla para voltar ao menu!");
            Console.ReadKey();
            Console.Clear();
            await new MenuPrincipal().Executar();
        }
        else
        {
            await new MenuPrincipal().Executar();
        }
    }
}
