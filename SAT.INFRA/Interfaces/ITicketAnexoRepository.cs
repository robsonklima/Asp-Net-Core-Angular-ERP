using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface ITicketAnexoRepository {
        PagedList<TicketAnexo> ObterPorParametros(TicketAnexoParameters parameters);
        TicketAnexo ObterPorCodigo(int codigo);
        TicketAnexo Criar(TicketAnexo anexo);
        TicketAnexo Deletar(int codigo);
        TicketAnexo Atualizar(TicketAnexo anexo);
    }
}