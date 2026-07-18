using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Funções;

internal class LerArquivo
{
   
    public static async Task LerOArquivo()
    {
        using(var fs = new FileStream("Cotacoes.txt", FileMode.Open))
        using (var leitor = new StreamReader(fs))
        {
            while (!leitor.EndOfStream)
            {
                var linha = leitor.ReadLine();
                Console.WriteLine(linha);
            }
        }
    }
}
