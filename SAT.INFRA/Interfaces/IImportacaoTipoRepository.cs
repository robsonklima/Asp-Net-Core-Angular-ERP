using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IImportacaoTipoRepository
    {
        ImportacaoTipo Criar(ImportacaoTipo importacaoConf);
        PagedList<ImportacaoTipo> ObterPorParametros(ImportacaoTipoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(ImportacaoTipo importacaoConf);
        ImportacaoTipo ObterPorCodigo(int codigo);
    }
}
