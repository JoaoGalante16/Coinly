namespace Coinly.Menus;

internal class MenuPrincipal : Menu
{
    public override async Task Executar()
    {
       await base.Executar();
        Console.WriteLine("Bem vindo! Selecione a operação que deseja!");
        Console.WriteLine("1. Cotar uma moeda");
        Console.WriteLine("2. Ver historico de moedas");
        Console.WriteLine("0. Sair\n");
        var resposta = int.TryParse(Console.ReadLine(), out int r) ? r : -1;
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
                Thread.Sleep(3000);
                break;
            default:
                Console.WriteLine("\nOpção inválida, tente novamente!");
                Thread.Sleep(3000);
                Console.Clear();
                await Executar();
                break;
        }
    }
}
