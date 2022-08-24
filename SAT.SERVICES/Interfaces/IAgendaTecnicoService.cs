using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;

namespace SAT.SERVICES.Interfaces
{
    public interface IAgendaTecnicoService
    {
        AgendaTecnico ObterPorCodigo(int codigo);
        List<ViewAgendaTecnicoRecurso> ObterViewPorParametros(AgendaTecnicoParameters parameters);
        List<AgendaTecnico> ObterPorParametros(AgendaTecnicoParameters parameters);
        AgendaTecnico Atualizar(AgendaTecnico agenda);
        void Criar(AgendaTecnico agenda);
    }
}