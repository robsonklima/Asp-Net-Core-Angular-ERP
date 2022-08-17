using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAuditoriaVeiculoAcessorioService
    {
       // ListViewModel ObterPorParametros(AuditoriaVeiculoAcessorioParameters parameters);
        AuditoriaVeiculoAcessorio Criar(AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio);
        void Deletar(int codigo);
        void Atualizar(AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio);
        AuditoriaVeiculoAcessorio ObterPorCodigo(int codigo);
    }
}
