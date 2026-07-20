using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Coinly.Funções;

internal class LerArquivo
{

    public static async Task<List<Cotacao>> LerOArquivo()
    {
        List<Cotacao> listaMoedas = new();
        using (var fs = new FileStream("Cotacoes.csv", FileMode.Open))
        using (var leitor = new StreamReader(fs))
        {
            while (!leitor.EndOfStream)
            {
                var linha = leitor.ReadLine();
                var cotacaoMoeda = Cotacao.ConverterStringParaCotacao(linha);
                listaMoedas.Add(cotacaoMoeda);
            }
            
        }
        return listaMoedas;
    }
}
