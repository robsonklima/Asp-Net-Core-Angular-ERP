using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IVersaoRepository
    {
        PagedList<Versao> ObterPorParametros(VersaoParameters parameters);
        void Criar(Versao Versao);
        void Atualizar(Versao Versao);
        void Deletar(int codVersao);
        Versao ObterPorCodigo(int codigo);
    }
}
