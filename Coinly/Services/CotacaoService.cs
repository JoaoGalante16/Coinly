using Coinly.Clients;
using Coinly.Modelos;
using Coinly.Utilities;

namespace Coinly.Services;

public class CotacaoService
{
    public static async Task ProcessarConsulta(string moeda)
    {
        try
        {
            var moedas = await ApiListaMoedasClient.CarregarMoedas();
            if (moedas is not null)
            {
                if (moedas.ContainsKey(moeda))
                {
                    var cotacao = await CotacaoApiClient.ApiCotar(moeda);
                    if (cotacao is not null)
                    {
                        Cotacao.MostrarCotacaoTabela();
                        cotacao.MostrarCotacao();
                        Console.WriteLine("--------------------------------------------");
                        await EscritorArquivo.EscreverNoArquivoCSV(cotacao);
                    }
                }
                else
                {
                    Console.WriteLine($"\n{moeda} não disponível\n");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Houve um erro: {ex.Message}");
        }
        
    }
}
