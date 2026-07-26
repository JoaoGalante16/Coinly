using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Modelos
{
    internal class MoedaAgrupada
    {
        public string Sigla { get; set; }
        public List<Cotacao> Cotacoes { get; set; }
        public int Total { get; set; }
    }
}
