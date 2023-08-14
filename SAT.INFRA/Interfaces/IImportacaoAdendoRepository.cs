using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IImportacaoAdendoRepository
    {
        PagedList<ImportacaoAdendo> ObterPorParametros(ImportacaoAdendoParameters parameters);
        ImportacaoAdendo Criar(ImportacaoAdendo data);
        ImportacaoAdendo Deletar(int cod);
        ImportacaoAdendo Atualizar(ImportacaoAdendo data);
        ImportacaoAdendo ObterPorCodigo(int cod);
    }
}
