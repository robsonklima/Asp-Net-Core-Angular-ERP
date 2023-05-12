using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IRedeBanrisulService
    {
        ListViewModel ObterPorParametros(RedeBanrisulParameters parameters);
        RedeBanrisul Criar(RedeBanrisul rede);
        RedeBanrisul Deletar(int codigo);
        RedeBanrisul Atualizar(RedeBanrisul rede);
        RedeBanrisul ObterPorCodigo(int codigo);
    }
}
