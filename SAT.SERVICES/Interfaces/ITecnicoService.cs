using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITecnicoService
    {
        ListViewModel ObterPorParametros(TecnicoParameters parameters);
        ListViewModel ObterDeslocamentos(TecnicoParameters parameters);
        Tecnico Criar(Tecnico tecnico);
        void Deletar(int codigo);
        void Atualizar(Tecnico tecnico);
        Tecnico ObterPorCodigo(int codigo);
    }
}
