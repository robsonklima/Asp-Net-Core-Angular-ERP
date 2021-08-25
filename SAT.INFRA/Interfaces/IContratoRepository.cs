using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IContratoRepository
    {
        void Criar(Contrato contrato);
        PagedList<Contrato> ObterPorParametros(ContratoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(Contrato contrato);
        Contrato ObterPorCodigo(int codigo);
    }
}
