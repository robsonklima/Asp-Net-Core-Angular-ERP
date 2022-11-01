using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.Views;

namespace SAT.INFRA.Interfaces {
    public interface IAuditoriaRepository {
        PagedList<Auditoria> ObterPorParametros(AuditoriaParameters parameters);
        Auditoria ObterPorCodigo(int CodAuditoria);
        void Criar(Auditoria auditoria);
        void Deletar(int codigoAuditoria);
        void Atualizar(Auditoria auditoria);
        PagedList<AuditoriaView> ObterPorView(AuditoriaParameters parameters);
    }
}