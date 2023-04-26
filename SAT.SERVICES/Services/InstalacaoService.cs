using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System.Linq;
using System;

namespace SAT.SERVICES.Services
{
    public class InstalacaoService : IInstalacaoService
    {
        private readonly IInstalacaoRepository _instalacaoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;
        private readonly IContratoEquipamentoRepository _contratoEquipRepo;
        private readonly IInstalacaoPagtoInstalRepository _instalPagtoIntalRepo;
        private readonly IInstalacaoPleitoInstalRepository _instalacaoPleitoInstalRepo;

        public InstalacaoService(
            IInstalacaoRepository instalacaoRepo,
            ISequenciaRepository sequenciaRepo,
            IContratoEquipamentoRepository ContratoEquipRepo,
            IInstalacaoPagtoInstalRepository instalPagtoIntalRepo,
            IInstalacaoPleitoInstalRepository instalacaoPleitoInstalRepo


        )
        {
            _instalacaoRepo = instalacaoRepo;
            _sequenciaRepo = sequenciaRepo;
            _contratoEquipRepo = ContratoEquipRepo;
            _instalPagtoIntalRepo = instalPagtoIntalRepo;
            _instalacaoPleitoInstalRepo = instalacaoPleitoInstalRepo;
        }

        public Instalacao ObterPorCodigo(int codigo)
        {
            return _instalacaoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(InstalacaoParameters parameters)
        {
            var instalacoes = _instalacaoRepo.ObterPorParametros(parameters);
            
            return new ListViewModel
            {
                Items = instalacoes,
                TotalCount = instalacoes.TotalCount,
                CurrentPage = instalacoes.CurrentPage,
                PageSize = instalacoes.PageSize,
                TotalPages = instalacoes.TotalPages,
                HasNext = instalacoes.HasNext,
                HasPrevious = instalacoes.HasPrevious
            };
        }

        public Instalacao Criar(Instalacao instalacao)
        {
            instalacao.CodInstalacao = _sequenciaRepo.ObterContador("Instalacao");
            _instalacaoRepo.Criar(instalacao);
            return instalacao;
        }

        public void Deletar(int codigo)
        {
            _instalacaoRepo.Deletar(codigo);
        }

        public void Atualizar(Instalacao instalacao)
        {
            _instalacaoRepo.Atualizar(instalacao);
        }
    }
}
