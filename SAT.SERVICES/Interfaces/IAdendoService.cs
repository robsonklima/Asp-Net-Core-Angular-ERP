using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAdendoService
    {
        ListViewModel ObterPorParametros(AdendoParameters parameters);
        Adendo Criar(Adendo adendo);
        Adendo Deletar(int codigo);
        Adendo Atualizar(Adendo adendo);
        Adendo ObterPorCodigo(int codigo);
    }
}
