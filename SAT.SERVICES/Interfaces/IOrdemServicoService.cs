using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrdemServicoService
    {
        ListViewModel ObterPorParametros(OrdemServicoParameters parameters);
        OrdemServico Criar(OrdemServico ordemServico);
        OrdemServico Atualizar(OrdemServico ordemServico);
        void Deletar(int codOS);
        OrdemServico ObterPorCodigo(int codigo);
        IActionResult ExportToExcel(OrdemServicoParameters parameters);
    }
}
