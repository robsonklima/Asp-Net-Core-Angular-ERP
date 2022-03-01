using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface IPecaRepository
    {
        PagedList<Peca> ObterPorParametros(PecaParameters parameters);
        IQueryable<Peca> ObterQuery(PecaParameters parameters);
        void Criar(Peca peca);
        void Atualizar(Peca peca);
        void Deletar(int codPeca);
        Peca ObterPorCodigo(int codigo);
    }
}
