using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IORTipoRepository
    {
        PagedList<ORTipo> ObterPorParametros(ORTipoParameters parameters);
        void Criar(ORTipo tipo);
        void Atualizar(ORTipo tipo);
        void Deletar(int codigo);
        ORTipo ObterPorCodigo(int codigo);
    }
}
