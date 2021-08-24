using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using Microsoft.AspNetCore.Authorization;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoContratoController : ControllerBase
    {
        private readonly IEquipamentoContratoRepository _equipamentoContratoInterface;
        private readonly ISequenciaRepository _sequenciaInterface;
        private readonly IEquipamentoRepository _equipamentoRepository;

        public EquipamentoContratoController(
            IEquipamentoContratoRepository equipamentoContratoInterface,
            ISequenciaRepository sequenciaInterface,
            IEquipamentoRepository equipamentoRepository
        )
        {
            _equipamentoContratoInterface = equipamentoContratoInterface;
            _sequenciaInterface = sequenciaInterface;
            _equipamentoRepository = equipamentoRepository;
        }

        [HttpGet]
        public EquipamentoContratoListViewModel Get([FromQuery] EquipamentoContratoParameters parameters)
        {
            var equipamentosContrato = _equipamentoContratoInterface.ObterPorParametros(parameters);

            var clienteListaViewModel = new EquipamentoContratoListViewModel
            {
                EquipamentosContrato = equipamentosContrato,
                TotalCount = equipamentosContrato.TotalCount,
                CurrentPage = equipamentosContrato.CurrentPage,
                PageSize = equipamentosContrato.PageSize,
                TotalPages = equipamentosContrato.TotalPages,
                HasNext = equipamentosContrato.HasNext,
                HasPrevious = equipamentosContrato.HasPrevious
            };

            return clienteListaViewModel;
        }

        [HttpGet("{codEquipContrato}")]
        public EquipamentoContrato Get(int codEquipContrato)
        {
            return _equipamentoContratoInterface.ObterPorCodigo(codEquipContrato);
        }

        [HttpPost]
        public EquipamentoContrato Post([FromBody] EquipamentoContrato equipamentoContrato)
        {
            equipamentoContrato.CodEquipContrato = this._sequenciaInterface.ObterContador(Constants.TABELA_EQUIPAMENTO_CONTRATO);
            var equip = _equipamentoRepository.ObterPorCodigo(equipamentoContrato.CodEquip);
            equipamentoContrato.CodTipoEquip = equip.CodTipoEquip;
            equipamentoContrato.CodGrupoEquip = equip.CodGrupoEquip;

            this._equipamentoContratoInterface.Criar(equipamentoContrato);
            return equipamentoContrato;
        }

        [HttpPut]
        public void Put([FromBody] EquipamentoContrato equipamentoContrato)
        {
            this._equipamentoContratoInterface.Atualizar(equipamentoContrato);
        }

        [HttpDelete("{codEquipContrato}")]
        public void Delete(int codEquipContrato)
        {
            this._equipamentoContratoInterface.Deletar(codEquipContrato);
        }
    }
}
