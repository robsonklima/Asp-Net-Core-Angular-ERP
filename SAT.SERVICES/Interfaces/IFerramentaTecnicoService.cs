using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IFerramentaTecnicoService
    {
        ListViewModel ObterPorParametros(FerramentaTecnicoParameters parameters);
        FerramentaTecnico Criar(FerramentaTecnico ferramentaTecnico);
        void Deletar(int codigo);
        void Atualizar(FerramentaTecnico ferramentaTecnico);
        FerramentaTecnico ObterPorCodigo(int codigo);
    }
}
