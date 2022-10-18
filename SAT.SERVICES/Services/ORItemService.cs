using System;
using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ORItemService : IORItemService
    {
        private readonly IORItemRepository _ORItemRepo;

        public ORItemService(IORItemRepository ORItemRepo)
        {
            _ORItemRepo = ORItemRepo;
        }

        public ListViewModel ObterPorParametros(ORItemParameters parameters)
        {
            var itens = _ORItemRepo.ObterPorParametros(parameters);
            itens = ObterDiasEmReparo(itens);

            var lista = new ListViewModel
            {
                Items = itens,
                TotalCount = itens.TotalCount,
                CurrentPage = itens.CurrentPage,
                PageSize = itens.PageSize,
                TotalPages = itens.TotalPages,
                HasNext = itens.HasNext,
                HasPrevious = itens.HasPrevious
            };

            return lista;
        }

        public ORItem Criar(ORItem item)
        {
            _ORItemRepo.Criar(item);

            return item;
        }

        public void Deletar(int codigo)
        {
            _ORItemRepo.Deletar(codigo);
        }

        public void Atualizar(ORItem item)
        {
            _ORItemRepo.Atualizar(item);
        }

        public ORItem ObterPorCodigo(int codigo)
        {
            return _ORItemRepo.ObterPorCodigo(codigo);
        }

        private PagedList<ORItem> ObterDiasEmReparo(PagedList<ORItem> itens) 
        {
            for (int i = 0; i < itens.Count; i++)
            {
                if (itens[i].DataConfLab.HasValue)
                    itens[i].DiasEmReparo = (int)(DateTime.Now - itens[i].DataConfLab.Value).TotalDays;
                else
                    itens[i].DiasEmReparo = (int)(DateTime.Now - itens[i].DataHoraCad).TotalDays;
            }

            return itens;
        }
    }
}
