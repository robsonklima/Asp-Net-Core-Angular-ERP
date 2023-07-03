using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IContratoServicoService
    {
        ListViewModel ObterPorParametros(ContratoServicoParameters parameters);
        ContratoServico ObterPorCodigo(int codContratoServico);
        ContratoServico Criar(ContratoServico contratoServico);
        void Deletar(int codigoContratoServico);
        void Atualizar(ContratoServico contratoServico);

    }
}
