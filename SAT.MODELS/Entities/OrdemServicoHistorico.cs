using System;

namespace SAT.MODELS.Entities
{
    public class OrdemServicoHistorico
    {
        public int CodHistOS { get; set; }
        public int? CodOS { get; set; }
        public int? CodTipoIntervencao { get; set; }
        public TipoIntervencao TipoIntervencao { get; set; }
        public int? CodStatusServico { get; set; }
        public StatusServico StatusServico { get; set; }
        public int? CodPosto { get; set; }
        public LocalAtendimento LocalAtendimento { get; set; }
        public int? CodEquipContrato { get; set; }
        public EquipamentoContrato EquipamentoContrato { get; set; }
        public int? CodTecnico { get; set; }
        public Tecnico Tecnico { get; set; }
        public DateTime DataHoraCad { get; set; }
        public int? CodCliente { get; set; }
        public Cliente Cliente { get; set; }
        public string CodUsuarioManutencao { get; set; }
        public Usuario Usuario { get; set; }
        public int? CodAutorizada { get; set; }
        public Autorizada Autorizada { get; set; }
    }
}