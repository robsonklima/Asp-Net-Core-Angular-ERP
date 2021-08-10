using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IMotivoAgendamentoRepository
    {
        PagedList<MotivoAgendamento> ObterPorParametros(MotivoAgendamentoParameters parameters);
        void Criar(MotivoAgendamento motivoAgendamento);
        void Deletar(int codigo);
        void Atualizar(MotivoAgendamento motivoAgendamento);
        MotivoAgendamento ObterPorCodigo(int codigo);
    }
}
