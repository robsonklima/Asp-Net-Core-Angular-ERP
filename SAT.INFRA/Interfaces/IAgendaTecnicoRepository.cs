using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAgendaTecnicoRepository
    {
        void Criar(AgendaTecnico agenda);
        PagedList<AgendaTecnico> ObterPorParametros(AgendaTecnicoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(AgendaTecnico agenda);
        AgendaTecnico ObterPorCodigo(int codigo);
    }
}
