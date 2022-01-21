using System;

namespace SAT.MODELS.Entities
{
    public class ImportacaoAberturaOrdemServico 
    {
        public string NomeFantasia { get; set; }
        public int? CodEquipContrato { get; set; }
        public string NumSerie { get; set; }
        public int? NumAgenciaBanco { get; set; }
        public int? DcPosto { get; set; }
        public string DefeitoRelatado { get; set; }
        public string TipoIntervencao { get; set; }
        public string NumOSQuarteirizada { get; set; }
        public string NumOSCliente { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }

    }
}