using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Funções;

internal class EscreverArquivo
{
    public static async Task EscreverNoArquivo(Cotacao cotacao)
    {
        using (var fs = new FileStream("Cotacoes.csv", FileMode.Append))
        using (var escritor = new StreamWriter(fs))
        {
            escritor.WriteLine($"{cotacao.Sigla},{cotacao.Valor},{cotacao.DataHora}");
        }
    }
}
