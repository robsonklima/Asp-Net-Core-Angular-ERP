using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class CausaImprodutividade
    {
        public int CodCausaImprodutividade { get; set; }
        public int CodImprodutividade { get; set; }
        public int CodProtocolo { get; set; }
        public int? IndAtivo { get; set; }
        public Improdutividade Improdutividade { get; set; }
        public ProtocoloChamadoSTN ProtocoloChamadoSTN { get; set; }

    }
}