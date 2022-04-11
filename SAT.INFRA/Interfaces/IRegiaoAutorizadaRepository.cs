using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IRegiaoAutorizadaRepository
    {
        PagedList<RegiaoAutorizada> ObterPorParametros(RegiaoAutorizadaParameters parameters);
        void Criar(RegiaoAutorizada regiaoAutorizada);
        void Atualizar(RegiaoAutorizada regiaoAutorizada, int codRegiao, int codAutorizada, int codFilial);
        void Deletar(int codRegiao, int codAutorizada, int codFilial);
        RegiaoAutorizada ObterPorCodigo(int codRegiao, int codAutorizada, int codFilial);
    }
}
