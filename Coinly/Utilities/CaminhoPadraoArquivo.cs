using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Utilities
{
    internal static class CaminhoPadraoArquivo
    {
        public static string RetornaCaminhoArquivo()
        {
            string nomeArquivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Coinly", "Cotacoes.csv");
            return nomeArquivo;
        }
    }
}
