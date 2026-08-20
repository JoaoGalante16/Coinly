using Coinly.Menus;
using Coinly.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Coinly.Funções;

public class ValidadorEntrada
{
    public static async Task<MatchCollection> ValidarEntrada(string entrada)
    {
        var regexValidacao = new Regex(@"^([A-Z]{3,4}),?\s?([A-Z]{3,4},?\s?)*?$");
        if (regexValidacao.IsMatch(entrada))
        {
            var regex = new Regex(@",?\s?([A-Z]{3,4})");
            var matches = regex.Matches(entrada);
            return matches;
        }
        else
        {
            Console.WriteLine("\nEntrada inválida!");
            Console.WriteLine("Voltando ao menu...");
            await Task.Delay(2000);
            Console.Clear();
            return null;
        }
    }
}
