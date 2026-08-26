using Coinly.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Coinly.Menus;

public abstract class Menu
{ 
    public virtual async Task Executar()
    {
        Console.Clear();
        Console.WriteLine("   ____      _       _\r\n  / ___|___ (_)_ __ | |_   _\r\n | |   / _ \\| | '_ \\| | | | |\r\n | |__| (_) | | | | | | |_| |\r\n  \\____\\___/|_|_| |_|_|\\__, |\r\n                       |___/\n");
    }

    public virtual async Task ExibirMensagemEntradaInvalida()
    {
        Console.WriteLine("Entrada inválida! Tente novamente!");
        await Task.Delay(2000);
        Console.Clear();
        await Executar();
    }

    public virtual void ExibirMensagemDeBusca()
    {

        Console.WriteLine("Digite as siglas das moedas que deseja buscar:");
        Console.WriteLine("Exemplo: BTC, USD, ETH\n");
    
    }

    public virtual void ExibirMensagemVoltarAoMenu()
    {
        Console.WriteLine("\nDigite qualquer tecla para voltar ao menu!");
        Console.ReadKey();
        Console.Clear();
    }
}
 

