using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface IAuditoriaStatusService
    {
        ListViewModel ObterPorParametros(AuditoriaStatusParameters parameters);
        AuditoriaStatus ObterPorCodigo(int codigo);
        void Criar(AuditoriaStatus auditoriaStatus);
        void Deletar(int codigoAuditoriaStatus);
        void Atualizar(AuditoriaStatus auditoriaStatus);
    }
}
