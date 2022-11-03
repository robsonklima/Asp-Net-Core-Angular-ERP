using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface IAuditoriaService
    {
        ListViewModel ObterPorParametros(AuditoriaParameters parameters);
        ListViewModel ObterPorView(AuditoriaParameters parameters);
        Auditoria ObterPorCodigo(int codigo);
        void Criar(Auditoria auditoria);
        void Deletar(int codigoAuditoria);
        void Atualizar(Auditoria auditoria);
    }
}
