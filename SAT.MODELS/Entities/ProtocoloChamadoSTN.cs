using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public OrdemServicoSTN OrdemServicoSTN { get; set; }

    }
}