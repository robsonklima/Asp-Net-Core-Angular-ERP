using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class EquipamentoService : IEquipamentoService
    {
        private readonly IEquipamentoRepository _equipRepo;

        public EquipamentoService(IEquipamentoRepository equipRepo)
        {
            _equipRepo = equipRepo;
        }

        public void Atualizar(Equipamento equipamento)
        {
            throw new System.NotImplementedException();
        }

        public Equipamento Criar(Equipamento equipamento)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public Equipamento ObterPorCodigo(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public ListViewModel ObterPorParametros(EquipamentoParameters parameters)
        {
            var causas = _equipRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = causas,
                TotalCount = causas.TotalCount,
                CurrentPage = causas.CurrentPage,
                PageSize = causas.PageSize,
                TotalPages = causas.TotalPages,
                HasNext = causas.HasNext,
                HasPrevious = causas.HasPrevious
            };

            return lista;
        }
    }
}
