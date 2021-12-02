
namespace SAT.MODELS.ViewModels
{
    public class DashboardTecnicoDisponibilidadeTecnicoViewModel
    {
        public bool TecnicoSemChamadosTransferidos { get; set; }
        public double MediaAtendimentosPorDiaCorretivos { get; set; }
        public double MediaAtendimentosPorDiaTodasIntervencoes { get; set; }
        public double MediaAtendimentosPorDiaPreventivos { get; set; }      
        public double MediaAtendimentosPorDiaInstalacoes { get; set; }  
        public double MediaAtendimentosPorDiaEngenharia { get; set; }

        public int IndFerias { get; set; }
        public byte? IndAtivo { get; set; }
        public int CodTecnico { get; set; }
        public int CodFilial { get; set; }
        public string NomeFilial { get; set; }
    }
}