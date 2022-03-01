using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoRepository
    {
        void Criar(Instalacao instalacao);
        PagedList<Instalacao> ObterPorParametros(InstalacaoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(Instalacao instalacao);
        Instalacao ObterPorCodigo(int codigo);
    }
}
