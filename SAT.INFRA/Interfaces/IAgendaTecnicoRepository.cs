using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAgendaTecnicoRepository
    {
        void CriarAgenda(AgendaTecnico agenda);
        PagedList<AgendaTecnico> ObterAgendasPorParametros(AgendaTecnicoParameters parameters);
        void DeletarAgenda(int codigo);
        void AtualizarAgenda(AgendaTecnico agenda);
        AgendaTecnico ObterAgendaPorCodigo(int codigo);
    }
}
