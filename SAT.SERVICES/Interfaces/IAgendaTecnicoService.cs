using SAT.MODELS.Entities;
using System.Collections.Generic;

namespace SAT.SERVICES.Interfaces
{
    public interface IAgendaTecnicoService
    {
        List<AgendaTecnico> ObterPorParametros(AgendaTecnicoParameters parameters);
        void Atualizar(AgendaTecnico agenda);
        void Deletar(int codigo);
        void Criar(AgendaTecnico agenda);
    }
}
