using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAutorizadaRepository
    {
        PagedList<Autorizada> ObterPorParametros(AutorizadaParameters parameters);
        void Criar(Autorizada autorizada);
        void Deletar(int codigo);
        void Atualizar(Autorizada autorizada);
        Autorizada ObterPorCodigo(int codigo);
    }
}
