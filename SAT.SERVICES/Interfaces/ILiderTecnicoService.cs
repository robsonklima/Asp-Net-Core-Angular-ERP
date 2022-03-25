using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface ILiderTecnicoService
    {
        ListViewModel ObterPorParametros(LiderTecnicoParameters parameters);
        LiderTecnico Criar(LiderTecnico liderTecnico);
        void Deletar(int codigo);
        void Atualizar(LiderTecnico liderTecnico);
        LiderTecnico ObterPorCodigo(int codigo);
    }
}
