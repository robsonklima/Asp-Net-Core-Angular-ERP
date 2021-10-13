using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAgendaTecnicoService
    {
        ListViewModel ObterPorParametros(AgendaTecnicoParameters parameters);
        AgendaTecnico Atualizar(AgendaTecnico agenda);
        void Deletar(int codigo);
        void Criar(AgendaTecnico agenda);
    }
}
