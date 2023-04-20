using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITecnicoContaService
    {
        ListViewModel ObterPorParametros(TecnicoContaParameters parameters);
        TecnicoConta Criar(TecnicoConta tecnico);
        void Deletar(int codigo);
        void Atualizar(TecnicoConta tecnico);
        TecnicoConta ObterPorCodigo(int codigo);
    }
}
