using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ContratoEquipamentoService : IContratoEquipamentoService
    {
        private readonly IContratoEquipamentoRepository _contratoEquipamentoRepo;

        public ContratoEquipamentoService(IContratoEquipamentoRepository contratoEquipamentoRepo)
        {
            _contratoEquipamentoRepo = contratoEquipamentoRepo;
        }

        public ListViewModel ObterPorParametros(ContratoEquipamentoParameters parameters)
        {
            var contratoEquipamentos = _contratoEquipamentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = contratoEquipamentos,
                TotalCount = contratoEquipamentos.TotalCount,
                CurrentPage = contratoEquipamentos.CurrentPage,
                PageSize = contratoEquipamentos.PageSize,
                TotalPages = contratoEquipamentos.TotalPages,
                HasNext = contratoEquipamentos.HasNext,
                HasPrevious = contratoEquipamentos.HasPrevious
            };

            return lista;
        }
        public ContratoEquipamento ObterPorCodigo(int codContrato, int codEquip)
        {
            return _contratoEquipamentoRepo.ObterPorCodigo(codContrato,codEquip);
        }

        public void Criar(ContratoEquipamento ContratoEquipamento)
        {
            _contratoEquipamentoRepo.Criar(ContratoEquipamento);
        }

        public void Deletar(int codigo)
        {
            _contratoEquipamentoRepo.Deletar(codigo);
        }

        public void Atualizar(ContratoEquipamento contratoEquipamento)
        {
            _contratoEquipamentoRepo.Atualizar(contratoEquipamento);
        }

    }
}
