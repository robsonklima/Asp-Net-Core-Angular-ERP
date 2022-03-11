using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IImportacaoConfiguracaoRepository
    {
        ImportacaoConfiguracao Criar(ImportacaoConfiguracao importacaoConf);
        PagedList<ImportacaoConfiguracao> ObterPorParametros(ImportacaoConfiguracaoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(ImportacaoConfiguracao importacaoConf);
        ImportacaoConfiguracao ObterPorCodigo(int codigo);
    }
}
