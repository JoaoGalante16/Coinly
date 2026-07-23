using Coinly.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Menus;

internal class MenuCotacao
{
    public static async Task MostrarMenuConsultar()
    {
        Console.Write("Sigla das moedas que deseja Cotar: ");
        Console.WriteLine("Exemplo: BTC, USD, ETH");
        var resposta = Console.ReadLine().ToUpper().Split(",").Select(c => c.Trim()).ToArray();

        await CotacaoService.ProcessarConsulta(resposta);
    }
}
