using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IORTransporteRepository
    {
        PagedList<ORTransporte> ObterPorParametros(ORTransporteParameters parameters);
        void Criar(ORTransporte ORTransporte);
        void Atualizar(ORTransporte ORTransporte);
        void Deletar(int codigo);
        ORTransporte ObterPorCodigo(int codigo);
    }
}
