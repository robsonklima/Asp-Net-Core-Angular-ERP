using SAT.MODELS.Entities;
using System.Collections.Generic;

namespace SAT.SERVICES.Interfaces
{
    public interface IAgendaTecnicoService
    {
        List<AgendaTecnico> ObterAgendaPorParametros(AgendaTecnicoParameters parameters);
        void AtualizarAgenda(AgendaTecnico agenda);
        void DeletarAgenda(int codigo);
    }
}
