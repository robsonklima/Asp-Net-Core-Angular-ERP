using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoRelatorioInstalacaoService : IOrdemServicoRelatorioInstalacaoService
    {
        private readonly IOrdemServicoRelatorioInstalacaoRepository _relatorioInstalacaoRepo;

        public OrdemServicoRelatorioInstalacaoService(IOrdemServicoRelatorioInstalacaoRepository relatorioAtendimentoRepo)
        {
            _relatorioInstalacaoRepo = relatorioAtendimentoRepo;
        }

        public ListViewModel ObterPorParametros(OrdemServicoRelatorioInstalacaoParameters parameters)
        {
            var relatorios = _relatorioInstalacaoRepo.ObterPorParametros(parameters);

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

        public OrdemServicoRelatorioInstalacao Criar(OrdemServicoRelatorioInstalacao relatorioInstalacao)
        {
            
            _relatorioInstalacaoRepo.Criar(relatorioInstalacao);

            return relatorioInstalacao;
        }

        public void Deletar(int codigo)
        {
            _relatorioInstalacaoRepo.Deletar(codigo);
        }

        public OrdemServicoRelatorioInstalacao Atualizar(OrdemServicoRelatorioInstalacao relatorioInstalacao)
        {    
            _relatorioInstalacaoRepo.Atualizar(relatorioInstalacao);

            return relatorioInstalacao;
        }

        public OrdemServicoRelatorioInstalacao ObterPorCodigo(int codigo)
        {
            return _relatorioInstalacaoRepo.ObterPorCodigo(codigo);
        }
    }
}
