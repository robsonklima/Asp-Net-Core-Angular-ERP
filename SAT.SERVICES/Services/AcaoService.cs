﻿using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AcaoService : IAcaoService
    {
        private readonly IAcaoRepository _acaoRepo;

        public AcaoService(IAcaoRepository acaoRepo)
        {
            _acaoRepo = acaoRepo;
        }

        public void Atualizar(Acao acao)
        {
            _acaoRepo.Atualizar(acao);
        }

        public void Criar(Acao acao)
        {
            _acaoRepo.Criar(acao);
        }

        public void Deletar(int codigo)
        {
            _acaoRepo.Deletar(codigo);
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
