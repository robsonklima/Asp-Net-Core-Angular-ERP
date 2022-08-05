using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IContratoServicoService
    {
        ListViewModel ObterPorParametros(ContratoServicoParameters parameters);
        ContratoServico ObterPorCodigo(int codigoContrato, int codContratoServico);
        void Criar(ContratoServico contratoServico);
        void Deletar(int codigoContrato, int codigoContratoServico);
        void Atualizar(ContratoServico contratoServico);

    }
}
