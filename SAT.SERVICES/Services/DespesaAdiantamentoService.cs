using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Views;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaAdiantamentoService : IDespesaAdiantamentoService
    {
        private readonly IDespesaAdiantamentoRepository _despesaAdiantamentoRepo;

        public DespesaAdiantamentoService(IDespesaAdiantamentoRepository despesaAdiantamentoRepo)
        {
            _despesaAdiantamentoRepo = despesaAdiantamentoRepo;
        }

        public void Atualizar(DespesaAdiantamento despesa)
        {
            _despesaAdiantamentoRepo.Atualizar(despesa);
        }

        public DespesaAdiantamento Criar(DespesaAdiantamento despesa)
        {
            _despesaAdiantamentoRepo.Criar(despesa);

            return despesa;
        }

        public DespesaAdiantamentoSolicitacao CriarSolicitacao(DespesaAdiantamentoSolicitacao solicitacao)
        {
            return _despesaAdiantamentoRepo.CriarSolicitacao(solicitacao);
        }

        public void Deletar(int codigo)
        {
            _despesaAdiantamentoRepo.Deletar(codigo);
        }

        public List<ViewMediaDespesasAdiantamento> ObterMediaAdiantamentos(int codTecnico)
        {
            return _despesaAdiantamentoRepo.ObterMediaAdiantamentos(codTecnico);
        }

        public DespesaAdiantamento ObterPorCodigo(int codigo)
        {
            return _despesaAdiantamentoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaAdiantamentoParameters parameters)
        {
            var despesaAdiantamento = _despesaAdiantamentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = despesaAdiantamento,
                TotalCount = despesaAdiantamento.TotalCount,
                CurrentPage = despesaAdiantamento.CurrentPage,
                PageSize = despesaAdiantamento.PageSize,
                TotalPages = despesaAdiantamento.TotalPages,
                HasNext = despesaAdiantamento.HasNext,
                HasPrevious = despesaAdiantamento.HasPrevious
            };

            return lista;
        }

        public ListViewModel ObterPorView(DespesaAdiantamentoParameters parameters)
        {
            var despesaAdiantamento = _despesaAdiantamentoRepo.ObterPorView(parameters);

            var lista = new ListViewModel
            {
                Items = despesaAdiantamento,
                TotalCount = despesaAdiantamento.TotalCount,
                CurrentPage = despesaAdiantamento.CurrentPage,
                PageSize = despesaAdiantamento.PageSize,
                TotalPages = despesaAdiantamento.TotalPages,
                HasNext = despesaAdiantamento.HasNext,
                HasPrevious = despesaAdiantamento.HasPrevious
            };

            return lista;
        }
    }
}