using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IUnidadeFederativaRepository
    {
        void Criar(UnidadeFederativa uf);
        PagedList<UnidadeFederativa> ObterPorParametros(UnidadeFederativaParameters parameters);
        void Deletar(int codigo);
        void Atualizar(UnidadeFederativa uf);
        UnidadeFederativa ObterPorCodigo(int codigo);
    }
}
