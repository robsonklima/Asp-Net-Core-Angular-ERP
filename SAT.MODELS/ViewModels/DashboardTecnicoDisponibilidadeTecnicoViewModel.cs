using SAT.MODELS.Entities;

namespace SAT.MODELS.ViewModels
{
    public class DashboardTecnicoDisponibilidadeTecnicoViewModel : Tecnico
    {
        public double MediaAtendimentosPorDiaPreventivos { get; set; }
        public double MediaAtendimentosPorDiaCorretivos { get; set; }
        public double MediaAtendimentosPorDiaInstalacoes { get; set; }
        public double MediaAtendimentosPorDiaEngenharia { get; set; }
        public double MediaAtendimentosPorDiaTodasIntervencoes { get; set; }
        public bool TecnicoSemChamadosTransferidos { get; set; }
    }
}
