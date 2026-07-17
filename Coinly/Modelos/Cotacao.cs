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
    public string Valor { get; set; }
    [JsonPropertyName("create_date")]
    public string DataHora { get; set; }

    public void MostrarCotacao()
    {
        Console.WriteLine($"Cotação de {Sigla}");
        Console.WriteLine($"Valor: {Valor}");
        Console.WriteLine($"Data: {DataHora}");
    }
}
