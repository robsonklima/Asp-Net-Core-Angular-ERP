using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ContratoEquipamentoDataService : IContratoEquipamentoDataService
    {
        private readonly IContratoEquipamentoDataRepository _contratoEquipamentoDataRepo;

        public ContratoEquipamentoDataService(IContratoEquipamentoDataRepository contratoEquipamentoRepo)
        {
            _contratoEquipamentoDataRepo = contratoEquipamentoRepo;
        }

        public ListViewModel ObterPorParametros(ContratoEquipamentoDataParameters parameters)
        {
            var contratoEquipData = _contratoEquipamentoDataRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = contratoEquipData,
                TotalCount = contratoEquipData.TotalCount,
                CurrentPage = contratoEquipData.CurrentPage,
                PageSize = contratoEquipData.PageSize,
                TotalPages = contratoEquipData.TotalPages,
                HasNext = contratoEquipData.HasNext,
                HasPrevious = contratoEquipData.HasPrevious
            };

            return lista;
        }

        public ContratoEquipamentoData Criar(ContratoEquipamentoData contratoEquipamentoData)
        {
            //_contratoEquipamentoRepo.Criar(contratoEquipamentoData);
            return contratoEquipamentoData;
        }

        public void Deletar(int codigo)
        {
            //_contratoEquipamentoRepo.Deletar(codigo);
        }

        public void Atualizar(ContratoEquipamentoData ContratoEquipamentoData)
        {
            //_contratoEquipamentoRepo.Atualizar(ContratoEquipamentoData);
        }

        public ContratoEquipamentoData ObterPorCodigo(int codigo)
        {
            return null; //_contratoEquipamentoRepo.ObterPorCodigo(codigo);
        }
    }
}
