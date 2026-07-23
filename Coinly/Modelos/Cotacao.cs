using Coinly.Chamadas;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinly.Modelos;

internal class Cotacao
{
    [JsonPropertyName("code")]
    public string Sigla { get; set; }
    [JsonPropertyName("bid")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public double Valor { get; set; }
    [JsonPropertyName("create_date")]
    public string DataHora { get; set; }
    [JsonPropertyName("timestamp")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public long Timestamp { get; set; }

    public void MostrarCotacao()
    {
        Console.WriteLine($"{Sigla,-10}{Valor,-15}{DataHora,-15}");
    }

    public static void MostrarCotacaoTabela()
    {
        Console.WriteLine($"{"Moeda",-10}{"Valor",-15}{"Data",-15}");
    }

    public static Cotacao ConverterStringParaCotacao(string linha)
    {
        string[] valores = linha.Split(',');
        var cotacaoMoeda = new Cotacao();
        cotacaoMoeda.Sigla = valores[0];
        cotacaoMoeda.Valor = double.Parse((valores[1].Replace('.', ',')));
        cotacaoMoeda.DataHora = valores[2];
        cotacaoMoeda.Timestamp = long.Parse(valores[3]);
        return cotacaoMoeda;
    }
}
