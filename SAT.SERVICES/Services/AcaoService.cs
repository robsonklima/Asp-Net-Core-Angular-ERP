using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public class AcaoService : IAcaoService
    {
        private readonly IAcaoRepository _acaoRepo;
        private readonly ISequenciaRepository _sequenciaRepository;

        public AcaoService(IAcaoRepository acaoRepo, ISequenciaRepository sequenciaRepository)
        {
            _acaoRepo = acaoRepo;
            this._sequenciaRepository = sequenciaRepository;
        }

        public void Atualizar(Acao acao)
        {
            _acaoRepo.Atualizar(acao);
        }

        public Acao Criar(Acao acao)
        {
            acao.CodAcao = this._sequenciaRepository.ObterContador("Acao");
            _acaoRepo.Criar(acao);

            return acao;
        }

        public void Deletar(int codigo)
        {
            _acaoRepo.Deletar(codigo);
        }

        public AcaoComponente ObterAcaoComponentePorCodigo(int codAcaoComponente)
        {
            return _acaoRepo.ObterAcaoComponentePorCodigo(codAcaoComponente);
        }

        public ListViewModel ObterListaAcaoComponente(AcaoParameters parameters)
        {
            var acoesComponente =  _acaoRepo.ObterListaAcaoComponente(parameters);

            var lista = new ListViewModel
            {
                Items = acoesComponente,
                TotalCount = acoesComponente.TotalCount,
                CurrentPage = acoesComponente.CurrentPage,
                PageSize = acoesComponente.PageSize,
                TotalPages = acoesComponente.TotalPages,
                HasNext = acoesComponente.HasNext,
                HasPrevious = acoesComponente.HasPrevious
            };

            return lista;
        }

        public Acao ObterPorCodigo(int codigo)
        {
            return _acaoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(AcaoParameters parameters)
        {
            var acoes = _acaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = acoes,
                TotalCount = acoes.TotalCount,
                CurrentPage = acoes.CurrentPage,
                PageSize = acoes.PageSize,
                TotalPages = acoes.TotalPages,
                HasNext = acoes.HasNext,
                HasPrevious = acoes.HasPrevious
            };

            return lista;
        }
    }
}
