using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface ICausaRepository
    {
        PagedList<Causa> ObterPorParametros(CausaParameters parameters);
        void Criar(Causa causa);
        void Atualizar(Causa causa);
        void Deletar(int codCausa);
        Causa ObterPorCodigo(int codigo);
    }
}
