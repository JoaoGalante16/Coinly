using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Coinly.Funções;

Console.Write("Sigla da moeda que deseja pesquisar: ");
string moeda = Console.ReadLine().ToUpper();
Console.Clear();

ValidadorMoeda validador = new();
await validador.CarregarMoedas();
await validador.ValidarMoeda(moeda);

