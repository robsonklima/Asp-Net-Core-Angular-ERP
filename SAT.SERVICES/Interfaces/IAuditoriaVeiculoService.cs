using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAuditoriaVeiculoService
    {
        ListViewModel ObterPorParametros(AuditoriaVeiculoParameters parameters);
        AuditoriaVeiculo Criar(AuditoriaVeiculo auditoriaVeiculo);
        void Deletar(int codigo);
        void Atualizar(AuditoriaVeiculo auditoriaVeiculo);
        AuditoriaVeiculo ObterPorCodigo(int codigo);
    }
}
