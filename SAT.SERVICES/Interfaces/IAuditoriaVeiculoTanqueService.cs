using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAuditoriaVeiculoTanqueService
    {
        //ListViewModel ObterPorParametros(AuditoriaVeiculoTanqueParameters parameters);
        AuditoriaVeiculoTanque Criar(AuditoriaVeiculoTanque auditoriaVeiculoTanque);
        void Deletar(int codigo);
        void Atualizar(AuditoriaVeiculoTanque auditoriaVeiculoTanque);
        AuditoriaVeiculoTanque ObterPorCodigo(int codigo);
    }
}
