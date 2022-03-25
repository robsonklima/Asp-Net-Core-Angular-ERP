using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IFerramentaTecnicoRepository
    {
        PagedList<FerramentaTecnico> ObterPorParametros(FerramentaTecnicoParameters parameters);
        void Criar(FerramentaTecnico ferramentaTecnico);
        void Atualizar(FerramentaTecnico ferramentaTecnico);
        void Deletar(int codFerramentaTecnico);
        FerramentaTecnico ObterPorCodigo(int codigo);
    }
}
