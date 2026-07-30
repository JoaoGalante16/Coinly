using Coinly.Chamadas;
using Coinly.Funções;
using Coinly.Modelos;

namespace Coinly.Services;

internal class CotacaoService
{
    public static async Task ProcessarConsulta(string moeda)
    {
        var moedas = await ChamadaMoedas.CarregarMoedas();
        if (moedas is not null)
        {
            if (moedas.ContainsKey(moeda))
            {
                var cotacao = await ChamadaCotacao.ApiCotar(moeda);
                if (cotacao is not null)
                {
                    Cotacao.MostrarCotacaoTabela();
                    cotacao.MostrarCotacao();
                    Console.WriteLine("--------------------------------------------");
                    await EscreverArquivo.EscreverNoArquivoCSV(cotacao);
                }
            }
            else
            {
                Console.WriteLine($"\n{moeda} não disponível\n");
            }
        }
    }
}
