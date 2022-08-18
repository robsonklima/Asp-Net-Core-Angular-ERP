using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface IAuditoriaStatusRepository {
        PagedList<AuditoriaStatus> ObterPorParametros(AuditoriaStatusParameters parameters);
        AuditoriaStatus ObterPorCodigo(int CodAuditoriaStatus);
        void Criar(AuditoriaStatus auditoriaStatus);
        void Deletar(int codigoAuditoriaStatus);
        void Atualizar(AuditoriaStatus auditoriaStatus);

    }
}