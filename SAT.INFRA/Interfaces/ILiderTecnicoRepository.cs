using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ILiderTecnicoRepository
    {
        PagedList<LiderTecnico> ObterPorParametros(LiderTecnicoParameters parameters);
        void Criar(LiderTecnico liderTecnico);
        void Atualizar(LiderTecnico liderTecnico);
        void Deletar(int codLiderTecnico);
        LiderTecnico ObterPorCodigo(int codigo);
    }
}
