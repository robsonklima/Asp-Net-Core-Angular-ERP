using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEquipamentoController : ControllerBase
    {
        private readonly ITipoEquipamentoRepository _tipoEquipamentoInterface;
        private readonly ISequenciaRepository _sequenciaInterface;

        public TipoEquipamentoController(
            ITipoEquipamentoRepository tipoEquipamentoInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _tipoEquipamentoInterface = tipoEquipamentoInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet]
        public TipoEquipamentoListViewModel Get([FromQuery] TipoEquipamentoParameters parameters)
        {
            var tiposEquipamento = _tipoEquipamentoInterface.ObterPorParametros(parameters);

            var tiposEquipamentoListaViewModel = new TipoEquipamentoListViewModel
            {
                TiposEquipamento = tiposEquipamento,
                TotalCount = tiposEquipamento.TotalCount,
                CurrentPage = tiposEquipamento.CurrentPage,
                PageSize = tiposEquipamento.PageSize,
                TotalPages = tiposEquipamento.TotalPages,
                HasNext = tiposEquipamento.HasNext,
                HasPrevious = tiposEquipamento.HasPrevious
            };

            return tiposEquipamentoListaViewModel;
        }

        [HttpGet("{codTipoEquip}")]
        public TipoEquipamento Get(int codTipoEquip)
        {
            return _tipoEquipamentoInterface.ObterPorCodigo(codTipoEquip);
        }

        [HttpPost]
        public void Post([FromBody] TipoEquipamento tipoEquipamento)
        {
            tipoEquipamento.CodTipoEquip = _sequenciaInterface.ObterContador(Constants.TABELA_TIPO_EQUIPAMENTO);

            _tipoEquipamentoInterface.Criar(tipoEquipamento: tipoEquipamento);
        }

        [HttpPut]
        public void Put([FromBody] TipoEquipamento tipoEquipamento)
        {
            _tipoEquipamentoInterface.Atualizar(tipoEquipamento);
        }

        [HttpDelete("{codTipoEquip}")]
        public void Delete(int codTipoEquip)
        {
            _tipoEquipamentoInterface.Deletar(codTipoEquip);
        }
    }
}
