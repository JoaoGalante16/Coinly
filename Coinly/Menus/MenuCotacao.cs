using Coinly.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Coinly.Menus;

internal class MenuCotacao
{
    public static async Task MostrarMenuConsultar()
    {
        Console.WriteLine("Digite a(s) sigla(s) da(s) moeda(s) que deseja cotar: ");
        Console.WriteLine("Exemplo: BTC, USD, ETH");
        var resposta = Console.ReadLine().ToUpper();
        var regexValidacao = new Regex(@"^([A-Z]{3,4}),?\s?([A-Z]{3,4},?\s?)*?$");
        if (regexValidacao.IsMatch(resposta))
        {
            var regex = new Regex(@",?\s?([A-Z]{3,4})");
            var matches = regex.Matches(resposta);
            if (matches is not null)
            {
                foreach (Match match in matches)
                {
                    var moeda = match.Groups[1].Value;
                    Console.WriteLine($"[{moeda}]");
                    //await CotacaoService.ProcessarConsulta(moeda);
                }
            }
        }
        else
        {
            Console.WriteLine("Entrada invalida, tente novamente");
            Thread.Sleep(5000);
            Console.Clear();
            await MostrarMenuConsultar();
        }
    }
}
