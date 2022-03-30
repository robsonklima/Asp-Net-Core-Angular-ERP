using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class ViewAgendaTecnicoRecurso
    {
        public int Id { get; set; }
        public string CodUsuario { get; set; }
        public string Nome { get; set; }
        public string FonePerto { get; set; }
        public int QtdChamadosTransferidos { get; set; }
        public int QtdChamadosAtendidos { get; set; }
        public IEnumerable<ViewAgendaTecnicoEvento> Eventos { get; set; }
    }
}