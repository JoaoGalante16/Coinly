using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Coinly.Utilities
{
    public class LeitorEntrada
    {
        public static async Task<MatchCollection> LerEValidarBusca()
        {
            var entrada = Console.ReadLine().ToUpper();
            var matches = await ValidadorEntrada.ValidarEntrada(entrada);
            return matches;
        }

        public static int LerOpcaoNumerica()
        {
            var resposta = int.TryParse(Console.ReadLine(), out int r) ? r : -1;
            return resposta;
        }
    }
}
