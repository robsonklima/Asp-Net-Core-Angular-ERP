using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IRedeBanrisulRepository
    {
        PagedList<RedeBanrisul> ObterPorParametros(RedeBanrisulParameters parameters);
        RedeBanrisul Criar(RedeBanrisul rede);
        RedeBanrisul Deletar(int codigo);
        RedeBanrisul Atualizar(RedeBanrisul rede);
        RedeBanrisul ObterPorCodigo(int codigo);
    }
}
