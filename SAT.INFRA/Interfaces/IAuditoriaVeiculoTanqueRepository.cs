using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAuditoriaVeiculoTanqueRepository
    {
       // PagedList<AuditoriaVeiculoTanque> ObterPorParametros(AuditoriaVeiculoTanqueParameters parameters);
        void Criar(AuditoriaVeiculoTanque auditoriaVeiculoTanque);
        void Deletar(int codigo);
        void Atualizar(AuditoriaVeiculoTanque auditoriaVeiculoTanque);
        AuditoriaVeiculoTanque ObterPorCodigo(int codigo);
    }
}
