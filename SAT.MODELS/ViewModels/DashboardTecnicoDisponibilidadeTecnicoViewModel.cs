using SAT.MODELS.Entities;

namespace SAT.MODELS.ViewModels
{
    public class DashboardTecnicoDisponibilidadeTecnicoViewModel : Tecnico
    {
        public int QtdChamadosAtendidosTodasIntervencoes { get; set; }
        public int QtdChamadosAtendidosSomenteCorretivos { get; set; }
        public int QtdChamadosAtendidosSomentePreventivos { get; set; }
        public int QtdChamadosAtendidosTodasIntervencoesDia { get; set; }        
    }
}
