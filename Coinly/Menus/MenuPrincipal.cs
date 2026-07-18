using Coinly.Funções;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Coinly.Menus;

internal class MenuPrincipal
{
    public async Task ExibirMenuPrincipal()
    {
        Console.WriteLine("   ____      _       _       \r\n  / ___|___ (_)_ __ | |_   _ \r\n | |   / _ \\| | '_ \\| | | | |\r\n | |__| (_) | | | | | | |_| |\r\n  \\____\\___/|_|_| |_|_|\\__, |\r\n                       |___/ ");
        Console.WriteLine("Bem vindo! Selecione a operação que deseja!");
        Console.WriteLine("1. Consultar cotação");
        Console.WriteLine("2. Ver historico");
        int resposta = int.Parse(Console.ReadLine());
        Console.Clear();
        switch (resposta)
        {
            case 1:
                await MenuCotacao.Consultar();
                break;
            case 2:
                await MenuHistorico.MostrarHistorico();
                break;
            default:
                Console.WriteLine("Opção invalida");
                break;
        }
    }
}
