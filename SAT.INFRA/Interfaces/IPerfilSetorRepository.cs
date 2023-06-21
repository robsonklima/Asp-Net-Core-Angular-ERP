using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface IPerfilSetorRepository
    {
        PagedList<PerfilSetor> ObterPorParametros(PerfilSetorParameters parameters);
        void Criar(PerfilSetor perfilSetor);
        void Deletar(int codigo);
        void Atualizar(PerfilSetor perfilSetor);
        PerfilSetor ObterPorCodigo(int codigo);
    }
}
