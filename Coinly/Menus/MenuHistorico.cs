using Coinly.Funções;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Menus;

internal class MenuHistorico
{
    public static async Task MostrarHistorico()
    {
        Console.WriteLine("Mostrando historico gravado no arquivo");
        await LerArquivo.LerOArquivo();
    }
}
