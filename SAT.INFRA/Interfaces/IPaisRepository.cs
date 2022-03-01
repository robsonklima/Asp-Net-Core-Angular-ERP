using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPaisRepository
    {
        PagedList<Pais> ObterPorParametros(PaisParameters parameters);
        void Criar(Pais pais);
        void Atualizar(Pais pais);
        void Deletar(int codPais);
        Pais ObterPorCodigo(int codigo);
    }
}
