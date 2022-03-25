using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAcordoNivelServicoRepository
    {
        PagedList<AcordoNivelServico> ObterPorParametros(AcordoNivelServicoParameters parameters);
        void Criar(AcordoNivelServico acordoNivelServico);
        void Atualizar(AcordoNivelServico acordoNivelServico);
        void Deletar(int codigo);
        AcordoNivelServico ObterPorCodigo(int codigo);
        void CriarLegado(AcordoNivelServicoLegado acordoNivelServicoLegado);
        void AtualizarLegado(AcordoNivelServicoLegado acordoNivelServicoLegado);
        void DeletarLegado(int codigo);
    }
}
