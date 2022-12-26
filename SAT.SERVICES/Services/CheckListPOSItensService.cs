using System;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class CheckListPOSItensService : ICheckListPOSItensService
    {
        private readonly ICheckListPOSItensRepository _checkListPOSItensRepo;

        public CheckListPOSItensService(
            ICheckListPOSItensRepository checkListPOSItensRepo
        )
        {
            _checkListPOSItensRepo = checkListPOSItensRepo;
        }

        public void Atualizar(CheckListPOSItens checkListPOSItens)
        {
            _checkListPOSItensRepo.Atualizar(checkListPOSItens);
        }

        public void Criar(CheckListPOSItens checkListPOSItens)
        {
            _checkListPOSItensRepo.Criar(checkListPOSItens);
        }

        public void Deletar(int codigoCheckListPOSItens)
        {
            _checkListPOSItensRepo.Deletar(codigoCheckListPOSItens);
        }

        public CheckListPOSItens ObterPorCodigo(int codCheckListPOSItens)
        {
            return _checkListPOSItensRepo.ObterPorCodigo(codCheckListPOSItens);
        }

        public ListViewModel ObterPorParametros(CheckListPOSItensParameters parameters)
        {
            var checkListPOSItenss = _checkListPOSItensRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = checkListPOSItenss,
                TotalCount = checkListPOSItenss.TotalCount,
                CurrentPage = checkListPOSItenss.CurrentPage,
                PageSize = checkListPOSItenss.PageSize,
                TotalPages = checkListPOSItenss.TotalPages,
                HasNext = checkListPOSItenss.HasNext,
                HasPrevious = checkListPOSItenss.HasPrevious
            };

            return lista;
        }

    }
}
