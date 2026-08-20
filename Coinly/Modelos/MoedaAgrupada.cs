namespace Coinly.Modelos;

public class MoedaAgrupada
{
    public string Sigla { get; set; }
    public List<Cotacao> Cotacoes { get; set; }
    public int Total { get; set; }
}
