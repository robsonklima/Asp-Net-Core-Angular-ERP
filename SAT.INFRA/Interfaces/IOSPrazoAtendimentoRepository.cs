using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOSPrazoAtendimentoRepository
    {
        PagedList<OSPrazoAtendimento> ObterPorParametros(OSPrazoAtendimentoParameters parameters);
        OSPrazoAtendimento Criar(OSPrazoAtendimento OsPrazoAtendimento);
        OSPrazoAtendimento Atualizar(OSPrazoAtendimento OsPrazoAtendimento);
        OSPrazoAtendimento Deletar(int codOsPrazoAtendimento);
        OSPrazoAtendimento ObterPorCodigo(int codigo);
    }
}
