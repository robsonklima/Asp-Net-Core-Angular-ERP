using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface ISetorRepository
    {
        PagedList<Setor> ObterPorParametros(SetorParameters parameters);
        void Criar(Setor setor);
        void Deletar(int codigo);
        void Atualizar(Setor setor);
        Setor ObterPorCodigo(int codigo);
    }
}
