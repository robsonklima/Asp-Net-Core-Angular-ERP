using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoRelatorioInstalacaoNaoConformidadeService : IOrdemServicoRelatorioInstalacaoNaoConformidadeService
    {
        private readonly IOrdemServicoRelatorioInstalacaoNaoConformidadeRepository _relatorioInstNaoConfRepo;

        public OrdemServicoRelatorioInstalacaoNaoConformidadeService(IOrdemServicoRelatorioInstalacaoNaoConformidadeRepository relatorioInstNaoConfRepo)
        {
            _relatorioInstNaoConfRepo = relatorioInstNaoConfRepo;
        }

        public ListViewModel ObterPorParametros(OrdemServicoRelatorioInstalacaoNaoConformidadeParameters parameters)
        {
            var relatorios = _relatorioInstNaoConfRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = relatorios,
                TotalCount = relatorios.TotalCount,
                CurrentPage = relatorios.CurrentPage,
                PageSize = relatorios.PageSize,
                TotalPages = relatorios.TotalPages,
                HasNext = relatorios.HasNext,
                HasPrevious = relatorios.HasPrevious
            };

            return lista;
        }

        public OrdemServicoRelatorioInstalacaoNaoConformidade Criar(OrdemServicoRelatorioInstalacaoNaoConformidade relatorioInstalacao)
        {
            
            _relatorioInstNaoConfRepo.Criar(relatorioInstalacao);

            return relatorioInstalacao;
        }

        public void Deletar(int codigo)
        {
            _relatorioInstNaoConfRepo.Deletar(codigo);
        }

        public OrdemServicoRelatorioInstalacaoNaoConformidade Atualizar(OrdemServicoRelatorioInstalacaoNaoConformidade relatorioInstalacao)
        {    
            _relatorioInstNaoConfRepo.Atualizar(relatorioInstalacao);

            return relatorioInstalacao;
        }

        public OrdemServicoRelatorioInstalacaoNaoConformidade ObterPorCodigo(int codigo)
        {
            return _relatorioInstNaoConfRepo.ObterPorCodigo(codigo);
        }
    }
}
