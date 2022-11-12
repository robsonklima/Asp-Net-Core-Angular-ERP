using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface ITicketAtendimentoRepository {
        PagedList<TicketAtendimento> ObterPorParametros(TicketAtendimentoParameters parameters);
        TicketAtendimento ObterPorCodigo(int codTicketAtendimento);
        TicketAtendimento Criar(TicketAtendimento atend);
        TicketAtendimento Deletar(int codigo);
        TicketAtendimento Atualizar(TicketAtendimento atend);
    }
}