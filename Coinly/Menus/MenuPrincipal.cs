using Coinly.Utilities;

namespace Coinly.Menus;

public class MenuPrincipal : Menu
{
    public override async Task Executar()
    {
        await base.Executar();
        Console.WriteLine("Bem vindo! Selecione a operação que deseja!");
        Console.WriteLine("1. Cotar uma moeda");
        Console.WriteLine("2. Ver historico de moedas");
        Console.WriteLine("0. Sair\n");
        var resposta = LeitorEntrada.LerOpcaoNumerica();
        switch (resposta)
        {
            case 1:
                await new MenuCotacao().Executar();
                break;
            case 2:
                await new MenuHistorico().Executar();
                break;
            case 0:
                Console.WriteLine("Saindo...");
                await Task.Delay(3000);
                break;
            default:
                await ExibirMensagemEntradaInvalida();
                break;
        }
    }
}
