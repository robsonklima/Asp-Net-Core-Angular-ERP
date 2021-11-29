using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ISLARepository
    {
        void Criar(SLA contrato);
        PagedList<SLA> ObterPorParametros(SLAParameters parameters);
        void Deletar(int codigo);
        void Atualizar(SLA contrato);
        SLA ObterPorCodigo(int codigo);
    }
}
