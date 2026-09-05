using Coinly.Clients;
using Coinly.Modelos;
using Coinly.Utilities;

namespace Coinly.Services;

public class CotacaoService
{
    public static async Task ProcessarConsulta(string moeda, HttpClient client)
    {
        try
        {
            var moedas = await ApiListaMoedasClient.CarregarMoedas(client);
            if (moedas is null)
            {
                Console.WriteLine($"\n{moeda} não disponível\n");
                return;
            }
            if (!moedas.ContainsKey(moeda))
            {
                Console.WriteLine($"\n{moeda} não disponível\n");
                return;
            }

            var cotacao = await ApiCotacaoClient.ApiCotar(moeda, client);
            if (cotacao is null)
            {
                return;
            }
            Cotacao.MostrarCotacaoTabela();
            cotacao.MostrarCotacao();
            Console.WriteLine("--------------------------------------------");
            await EscritorArquivo.EscreverNoArquivoCSV(cotacao);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Houve um erro: {ex.Message}");
        }

    }
}
