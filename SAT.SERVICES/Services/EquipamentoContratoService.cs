using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class EquipamentoContratoService : IEquipamentoContratoService
    {
        private readonly IEquipamentoContratoRepository _equipamentoContratoRepo;
        private readonly IEquipamentoRepository _equipamentoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public EquipamentoContratoService(
            IEquipamentoContratoRepository equipamentoContratoRepo,
            IEquipamentoRepository equipamentoRepo,
            ISequenciaRepository seqRepo
        )
        {
            _equipamentoContratoRepo = equipamentoContratoRepo;
            _equipamentoRepo = equipamentoRepo;
            _seqRepo = seqRepo;
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
            equipamentoContrato.CodEquipContrato = this._seqRepo.ObterContador(Constants.TABELA_EQUIPAMENTO_CONTRATO);
            var equip = _equipamentoRepo.ObterPorCodigo(equipamentoContrato.CodEquip);
            equipamentoContrato.CodTipoEquip = equip.CodTipoEquip;
            equipamentoContrato.CodGrupoEquip = equip.CodGrupoEquip;

            _equipamentoContratoRepo.Criar(equipamentoContrato);
            return equipamentoContrato;
        }

        public void Deletar(int codigo)
        {
            _equipamentoContratoRepo.Deletar(codigo);
        }

        public void Atualizar(EquipamentoContrato equipamentoContrato)
        {
            var equip = _equipamentoRepo.ObterPorCodigo(equipamentoContrato.CodEquip);
            equipamentoContrato.CodTipoEquip = equip.CodTipoEquip;
            equipamentoContrato.CodGrupoEquip = equip.CodGrupoEquip;
            _equipamentoContratoRepo.Atualizar(equipamentoContrato);
        }

        public EquipamentoContrato ObterPorCodigo(int codigo)
        {
            return _equipamentoContratoRepo.ObterPorCodigo(codigo);
        }
    }
}
