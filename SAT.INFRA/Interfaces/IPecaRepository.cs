using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPecaRepository
    {
        PagedList<Peca> ObterPorParametros(PecaParameters parameters);
        void Criar(Peca peca);
        void Atualizar(Peca peca);
        void Deletar(int codPeca);
        Peca ObterPorCodigo(int codigo);
    }
}
