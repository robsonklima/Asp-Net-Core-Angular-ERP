using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.SERVICES.Interfaces
{
    public interface IAgendaTecnicoService
    {
        ListViewModel ObterPorParametros(AgendaTecnicoParameters parameters);
        void Atualizar(AgendaTecnico agenda);
        void Deletar(int codigo);
        void Criar(AgendaTecnico agenda);
    }
}
