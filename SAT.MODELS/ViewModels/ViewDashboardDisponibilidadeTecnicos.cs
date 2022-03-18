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
        public double? MediaAtendimentoTecnicoDiaTodasIntervencoes { get; set; }
        public double? MediaAtendimentoTecnicoDiaCorretivas { get; set; }
        public double? MediaAtendimentoTecnicoDiaPreventivas { get; set; }
    }
}
