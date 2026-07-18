using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Funções;

internal class Arquivo
{
    public static async Task EscreverNoArquivo(Cotacao cotacao)
    {
        using(var fs = new FileStream("Cotacoes.txt",FileMode.Append))
        using(var escritor = new StreamWriter(fs))
        {
            escritor.WriteLine($"{cotacao.Sigla},{cotacao.Valor},{cotacao.DataHora}");
        }
    }

    public static async Task LerArquivo()
    {
        using(var fs = new FileStream("Cotacoes.txt", FileMode.Open))
        using (var leitor = new StreamReader(fs))
        {
            while (!leitor.EndOfStream)
            {
                leitor.ReadLine();
            }
        }
    }
}
