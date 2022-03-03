using System;

namespace SAT.MODELS.ViewModels
{
    public class ViewDashboardDisponibilidadeTecnicos
    {
        public string Filial { get; set; }
        public int? TecnicosComChamados { get; set; }
        public int? TecnicosSemChamados { get; set; }
        public int? TecnicosInativos { get; set; }
        public int? TecnicosTotal { get; set; }
        public int? QtdOSNaoTransferidasCorretivas { get; set; }
        public int? MediaAtendimentoTecnicoDiaTodasIntervencoes { get; set; }
        public int? MediaAtendimentoTecnicoDiaCorretivas { get; set; }
        public int? MediaAtendimentoTecnicoDiaPreventivas { get; set; }
    }
}
