using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Interfaces
{
    public interface IAgendaTecnicoRepository
    {
        AgendaTecnico Criar(AgendaTecnico agenda);
        List<ViewAgendaTecnicoEvento> ObterViewPorParametros(AgendaTecnicoParameters parameters);
        List<AgendaTecnico> ObterPorParametros(AgendaTecnicoParameters parameters);
        AgendaTecnico Atualizar(AgendaTecnico agenda);
        AgendaTecnico ObterPorCodigo(int codigo);
    }
}
