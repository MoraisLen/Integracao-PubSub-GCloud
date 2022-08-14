using Boletador.Model.Enum;

namespace Boletador.Model
{
    public class BoletoModel
    {
        public string NomeContraparte { get; set; }
        public string CPF { get; set; }
        public decimal ValorGeral { get; set; }
        public TipoBoletoEnum TipoBoleto { get; set; }
    }
}