using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAgendaTecnicoService
    {
        AgendaTecnico ObterPorCodigo(int codigo);
        List<ViewAgendaTecnicoRecurso> ObterPorParametros(AgendaTecnicoParameters parameters);
        AgendaTecnico Atualizar(AgendaTecnico agenda);
        void Deletar(int codigo);
        void Criar(AgendaTecnico agenda);
    }
}