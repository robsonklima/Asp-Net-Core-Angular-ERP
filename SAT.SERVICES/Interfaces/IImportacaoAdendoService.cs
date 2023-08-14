using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IImportacaoAdendoService
    {
        ListViewModel ObterPorParametros(ImportacaoAdendoParameters parameters);
        ImportacaoAdendo Criar(ImportacaoAdendo adendo);
        ImportacaoAdendo Deletar(int codigo);
        ImportacaoAdendo Atualizar(ImportacaoAdendo adendo);
        ImportacaoAdendo ObterPorCodigo(int codigo);
    }
}
