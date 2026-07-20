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
                string[] valores = linha.Split(',');
                var cotacaoMoeda = new Cotacao();
                cotacaoMoeda.Sigla = valores[0];
                cotacaoMoeda.Valor = valores[1];
                cotacaoMoeda.DataHora = valores[2];
                listaMoedas.Add(cotacaoMoeda);
                Console.WriteLine("------------------");
                cotacaoMoeda.MostrarCotacao();
            }
            
        }
        return listaMoedas;
    }
}
