using SAT.MODELS.Entities;

namespace SAT.MODELS.ViewModels
{
    public class DashboardTecnicoDisponibilidadeTecnicoViewModel : Tecnico
    {
        public bool TecnicoSemChamadosTransferidos { get; set; }
        public double MediaAtendimentosPorDiaCorretivos { get; set; }
        public double MediaAtendimentosPorDiaTodasIntervencoes { get; set; }
        public double MediaAtendimentosPorDiaPreventivos { get; set; }      
        public double MediaAtendimentosPorDiaInstalacoes { get; set; }  
        public double MediaAtendimentosPorDiaEngenharia { get; set; }
    }
}