using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class ImportacaoAberturaOrdemServico
    {
        public string NomeFantasia { get; set; }
        public string NumSerie { get; set; }
        public string NumAgenciaBanco { get; set; }
        public string DcPosto { get; set; }
        public string DefeitoRelatado { get; set; }
        public string TipoIntervencao { get; set; }
        public string NumOSQuarteirizada { get; set; }
        public string NumOSCliente { get; set; }
    }
}