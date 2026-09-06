using Coinly.Modelos;
using System.Globalization;
using System.Text.Json;

namespace Coinly.Utilities;

public class EscritorArquivo
{

    public virtual async Task EscreverNoArquivoCSV(Cotacao cotacao)
    {
        try
        {
            var pasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Coinly");
            Directory.CreateDirectory(pasta);
            var nomeArquivo = CaminhoPadraoArquivo.RetornaCaminhoArquivo();
            using (var fs = new FileStream(nomeArquivo, FileMode.Append))
            using (var escritor = new StreamWriter(fs))
            {
                await escritor.WriteLineAsync($"{cotacao.Sigla},{cotacao.Valor.ToString(CultureInfo.InvariantCulture)},{cotacao.DataHora},{cotacao.Timestamp}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Houve um erro ao esver o arquivo, {ex.Message}");
        }
    }

    public static async Task<string> EscreverNoArquivoJson(List<MoedaAgrupada> cotacoes)
    {
        try
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
        catch (Exception ex)
        {
            return $"Não foi possível criar o arquivo de cotações, {ex.Message}";
        }
    }
}
