using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class ProtocoloChamadoSTN
    {
        public int CodProtocoloChamadoSTN { get; set; }
        public int CodAtendimento { get; set; }
        public int? CodTipoChamadoSTN { get; set; }
        public int IndPrimeiraLigacao { get; set; }
        public string AcaoSTN { get; set; }
        public string TecnicoCampo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public int? IndAtivo { get; set; }
        public TipoChamadoSTN TipoChamadoSTN { get; set; }
        public Usuario Usuario { get; set; }
        public List<CausaImprodutividade> CausaImprodutividades { get; set; }

    }
}