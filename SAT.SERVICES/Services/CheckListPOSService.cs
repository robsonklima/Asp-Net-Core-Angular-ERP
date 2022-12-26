using System;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class CheckListPOSService : ICheckListPOSService
    {
        private readonly ICheckListPOSRepository _checkListPOSRepo;

        public CheckListPOSService(
            ICheckListPOSRepository checkListPOSRepo
        )
        {
            _checkListPOSRepo = checkListPOSRepo;
        }

        public void Atualizar(CheckListPOS checkListPOS)
        {
            _checkListPOSRepo.Atualizar(checkListPOS);
        }

        public void Criar(CheckListPOS checkListPOS)
        {
            _checkListPOSRepo.Criar(checkListPOS);
        }

        public void Deletar(int codigoCheckListPOS)
        {
            _checkListPOSRepo.Deletar(codigoCheckListPOS);
        }

        public CheckListPOS ObterPorCodigo(int codCheckListPOS)
        {
            return _checkListPOSRepo.ObterPorCodigo(codCheckListPOS);
        }

        public ListViewModel ObterPorParametros(CheckListPOSParameters parameters)
        {
            var checkListPOSs = _checkListPOSRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = checkListPOSs,
                TotalCount = checkListPOSs.TotalCount,
                CurrentPage = checkListPOSs.CurrentPage,
                PageSize = checkListPOSs.PageSize,
                TotalPages = checkListPOSs.TotalPages,
                HasNext = checkListPOSs.HasNext,
                HasPrevious = checkListPOSs.HasPrevious
            };

            return lista;
        }

    }
}
