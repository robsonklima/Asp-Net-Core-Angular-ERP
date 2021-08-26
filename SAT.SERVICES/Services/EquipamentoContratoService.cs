using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class EquipamentoContratoService : IEquipamentoContratoService
    {
        private readonly IEquipamentoContratoRepository _equipamentoContratoRepo;

        public EquipamentoContratoService(IEquipamentoContratoRepository equipamentoContratoRepo)
        {
            _equipamentoContratoRepo = equipamentoContratoRepo;
        }

        public ListViewModel ObterPorParametros(EquipamentoContratoParameters parameters)
        {
            var equipamentoContratos = _equipamentoContratoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = equipamentoContratos,
                TotalCount = equipamentoContratos.TotalCount,
                CurrentPage = equipamentoContratos.CurrentPage,
                PageSize = equipamentoContratos.PageSize,
                TotalPages = equipamentoContratos.TotalPages,
                HasNext = equipamentoContratos.HasNext,
                HasPrevious = equipamentoContratos.HasPrevious
            };

            return lista;
        }

        public EquipamentoContrato Criar(EquipamentoContrato equipamentoContrato)
        {
            _equipamentoContratoRepo.Criar(equipamentoContrato);
            return equipamentoContrato;
        }

        public void Deletar(int codigo)
        {
            _equipamentoContratoRepo.Deletar(codigo);
        }

        public void Atualizar(EquipamentoContrato EquipamentoContrato)
        {
            _equipamentoContratoRepo.Atualizar(EquipamentoContrato);
        }

        public EquipamentoContrato ObterPorCodigo(int codigo)
        {
            return _equipamentoContratoRepo.ObterPorCodigo(codigo);
        }
    }
}
