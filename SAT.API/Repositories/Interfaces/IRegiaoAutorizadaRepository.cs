using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IRegiaoAutorizadaRepository
    {
        PagedList<RegiaoAutorizada> ObterPorParametros(RegiaoAutorizadaParameters parameters);
        void Criar(RegiaoAutorizada regiaoAutorizada);
        void Atualizar(RegiaoAutorizada regiaoAutorizada);
        void Deletar(int codRegiao, int codAutorizada, int codFilial);
        RegiaoAutorizada ObterPorCodigo(int codRegiao, int codAutorizada, int codFilial);
    }
}
