using System;

namespace SAT.MODELS.Entities
{
    public class OrdemServicoHistorico
    {
        public int CodHistOS { get; set; }
        public int? CodOS { get; set; }
        public int? CodTipoIntervencao { get; set; }
        public int? CodStatusServico { get; set; }
        public int? CodPosto { get; set; }
        public int? CodEquipContrato { get; set; }
        public int? CodTecnico { get; set; }
        public DateTime DataHoraCad { get; set; }
        public int? CodCliente { get; set; }
        public string CodUsuarioManutencao { get; set; }
        public int? CodAutorizada { get; set; }
    }
}