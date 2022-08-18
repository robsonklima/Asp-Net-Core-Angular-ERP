using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAuditoriaVeiculoRepository
    {
        PagedList<AuditoriaVeiculo> ObterPorParametros(AuditoriaVeiculoParameters parameters);
        void Criar(AuditoriaVeiculo auditoriaVeiculo);
        void Deletar(int codigo);
        void Atualizar(AuditoriaVeiculo auditoriaVeiculo);
        AuditoriaVeiculo ObterPorCodigo(int codigo);
    }
}
