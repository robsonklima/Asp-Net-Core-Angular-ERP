using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDispBBBloqueioOSRepository
    {
        void Criar(DispBBBloqueioOS dispBBBloqueioOS);
        PagedList<DispBBBloqueioOS> ObterPorParametros(DispBBBloqueioOSParameters parameters);
        void Deletar(int codigo);
        void Atualizar(DispBBBloqueioOS dispBBBloqueioOS);
        DispBBBloqueioOS ObterPorCodigo(int CodOS);
    }
}
