using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IOSPrazoAtendimentoService
    {
        ListViewModel ObterPorParametros(OSPrazoAtendimentoParameters parameters);
        OSPrazoAtendimento Criar(OSPrazoAtendimento prazo);
        OSPrazoAtendimento Deletar(int codigo);
        OSPrazoAtendimento Atualizar(OSPrazoAtendimento prazo);
        OSPrazoAtendimento ObterPorCodigo(int codigo);
    }
}
