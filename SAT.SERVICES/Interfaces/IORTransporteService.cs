using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IORTransporteService
    {
        ListViewModel ObterPorParametros(ORTransporteParameters parameters);
        ORTransporte Criar(ORTransporte ORTransporte);
        void Deletar(int codigo);
        void Atualizar(ORTransporte ORTransporte);
        ORTransporte ObterPorCodigo(int codigo);
    }
}
