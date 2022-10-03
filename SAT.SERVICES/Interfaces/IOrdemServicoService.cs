using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using System.Linq;
using System;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrdemServicoService
    {
        ListViewModel ObterPorParametros(OrdemServicoParameters parameters);
        OrdemServico Criar(OrdemServico ordemServico);
        OrdemServico Atualizar(OrdemServico ordemServico);
        void Deletar(int codOS);
        OrdemServico ObterPorCodigo(int codigo);
        OrdemServico Clonar(OrdemServico ordemServico);
    }
}