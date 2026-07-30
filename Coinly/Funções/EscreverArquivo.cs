using Coinly.Modelos;
using System.Globalization;
using System.Text.Json;

namespace Coinly.Funções;

internal class EscreverArquivo
{
    
    public static async Task EscreverNoArquivoCSV(Cotacao cotacao)
    {
        var pasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Coinly");
        Directory.CreateDirectory(pasta);
        var nomeArquivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Coinly", "Cotacoes.csv");
        using (var fs = new FileStream(nomeArquivo, FileMode.Append))
        using (var escritor = new StreamWriter(fs))
        {
            escritor.WriteLine($"{cotacao.Sigla},{cotacao.Valor.ToString(CultureInfo.InvariantCulture)},{cotacao.DataHora},{cotacao.Timestamp}");
            
        }
    }

    public static async Task<string> EscreverNoArquivoJson(List<MoedaAgrupada> cotacoes)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var nomeArquivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Cotacoes.json");
        using (var fs = new FileStream(nomeArquivo, FileMode.Create, FileAccess.Write))
           await JsonSerializer.SerializeAsync(fs, cotacoes, jsonOptions);

        return $"\n\nArquivo criado com as cotações em{nomeArquivo}\n";
    }
}
