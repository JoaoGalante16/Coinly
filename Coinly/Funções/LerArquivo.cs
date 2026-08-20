using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Coinly.Funções;

public class LerArquivo
{

    public static async Task<List<Cotacao>> LerOArquivo()
    {
        try
        {
            List<Cotacao> listaMoedas = new();
            var nomeArquivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Coinly", "Cotacoes.csv");
            if (!File.Exists(nomeArquivo))
            {
                return listaMoedas;
            }
            using (var fs = new FileStream(nomeArquivo, FileMode.Open))
            using (var leitor = new StreamReader(fs))
            {
                while (!leitor.EndOfStream)
                {
                    var linha = await leitor.ReadLineAsync();
                    var cotacaoMoeda = Cotacao.ConverterStringParaCotacao(linha);
                    listaMoedas.Add(cotacaoMoeda);
                }

            }
            return listaMoedas;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Houve um erro ao tentar ler o arquivo {ex.Message}");
            return null;
        }
    }
}
