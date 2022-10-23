using System;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoSTNOrigemService : IOrdemServicoSTNOrigemService
    {
        private readonly IOrdemServicoSTNOrigemRepository _OrdemServicoSTNOrigemRepo;

        public OrdemServicoSTNOrigemService(
            IOrdemServicoSTNOrigemRepository OrdemServicoSTNOrigemRepo
        )
        {
            _OrdemServicoSTNOrigemRepo = OrdemServicoSTNOrigemRepo;
        }

        public ListViewModel ObterPorParametros(OrdemServicoSTNOrigemParameters parameters)
        {
            var ordens = _OrdemServicoSTNOrigemRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ordens,
                TotalCount = ordens.TotalCount,
                CurrentPage = ordens.CurrentPage,
                PageSize = ordens.PageSize,
                TotalPages = ordens.TotalPages,
                HasNext = ordens.HasNext,
                HasPrevious = ordens.HasPrevious
            };

            return lista;
        }

        public OrdemServicoSTNOrigem ObterPorCodigo(int codigo)
        {
            return _OrdemServicoSTNOrigemRepo.ObterPorCodigo(codigo);
        }
    }
}
