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
        private readonly ISequenciaRepository _sequenciaRepository;

        public EquipamentoService(IEquipamentoRepository equipRepo, ISequenciaRepository sequenciaRepository)
        {
            _equipRepo = equipRepo;
            this._sequenciaRepository = sequenciaRepository;
        }

        public void Atualizar(Equipamento equipamento)
        {
            this._equipRepo.Atualizar(equipamento);
        }

        public Equipamento Criar(Equipamento equipamento)
        {
            equipamento.CodEquip = this._sequenciaRepository.ObterContador("Equipamento");
            this._equipRepo.Criar(equipamento);
            return equipamento;
        }

        public void Deletar(int codigo)
        {
            this._equipRepo.Deletar(codigo);
        }

        public Equipamento ObterPorCodigo(int codigo)
        {
           return this._equipRepo.ObterPorCodigo(codigo);
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
