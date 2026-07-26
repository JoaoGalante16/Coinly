using Coinly.Modelos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace Coinly.Funções;

internal class EscreverArquivo
{
    
    public static async Task EscreverNoArquivoCSV(Cotacao cotacao)
    {
        var nomeArquivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Coinly", "Cotacoes.csv");
        using (var fs = new FileStream(nomeArquivo, FileMode.Append))
        using (var escritor = new StreamWriter(fs))
        {
            escritor.WriteLine($"{cotacao.Sigla},{cotacao.Valor.ToString(CultureInfo.InvariantCulture)},{cotacao.DataHora},{cotacao.Timestamp}");
            
        }
    }

    public static async Task EscreverNoArquivoJson(List<MoedaAgrupada> cotacoes)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var nomeArquivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Cotacoes.json");
        using (var fs = new FileStream(nomeArquivo, FileMode.Create, FileAccess.Write))
            JsonSerializer.Serialize(fs, cotacoes, jsonOptions);
    }
}
