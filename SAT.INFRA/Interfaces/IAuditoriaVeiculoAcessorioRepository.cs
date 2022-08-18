using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAuditoriaVeiculoAcessorioRepository
    {
        PagedList<AuditoriaVeiculoAcessorio> ObterPorParametros(AuditoriaVeiculoAcessorioParameters parameters);
        void Criar(AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio);
        void Deletar(int codigo);
        void Atualizar(AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio);
        AuditoriaVeiculoAcessorio ObterPorCodigo(int codigo);
    }
}
