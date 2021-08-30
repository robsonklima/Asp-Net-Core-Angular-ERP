using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IOrdemServicoRepository
    {
        PagedList<OrdemServico> ObterPorParametros(OrdemServicoParameters parameters);
        void Criar(OrdemServico ordemServico);
        void Atualizar(OrdemServico ordemServico);
        void Deletar(int codOS);
        OrdemServico ObterPorCodigo(int codigo);
    }
}
