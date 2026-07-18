using Coinly.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Menus;

internal class MenuCotacao
{
    public static async Task Consultar()
    {
        Console.Write("Sigla da moeda que deseja pesquisar: ");
        string moeda = Console.ReadLine().ToUpper();

        CotacaoService cotacao = new CotacaoService();
        await cotacao.ProcessarConsulta(moeda);
    }
}
