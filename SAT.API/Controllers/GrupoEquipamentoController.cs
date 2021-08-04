using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoEquipamentoController : ControllerBase
    {
        private IGrupoEquipamentoRepository _grupoEquipamentoInterface;
        private readonly ISequenciaRepository _sequenciaInterface;

        public GrupoEquipamentoController(
            IGrupoEquipamentoRepository grupoEquipamentoInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _grupoEquipamentoInterface = grupoEquipamentoInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet]
        public GrupoEquipamentoListViewModel Get([FromQuery] GrupoEquipamentoParameters parameters)
        {
            var gruposEquipamento = _grupoEquipamentoInterface.ObterPorParametros(parameters);

            var grupoEquipamentoListaViewModel = new GrupoEquipamentoListViewModel
            {
                GruposEquipamento = gruposEquipamento,
                TotalCount = gruposEquipamento.TotalCount,
                CurrentPage = gruposEquipamento.CurrentPage,
                PageSize = gruposEquipamento.PageSize,
                TotalPages = gruposEquipamento.TotalPages,
                HasNext = gruposEquipamento.HasNext,
                HasPrevious = gruposEquipamento.HasPrevious
            };

            return grupoEquipamentoListaViewModel;
        }

        [HttpGet("{codGrupoEquip}")]
        public GrupoEquipamento Get(int codGrupoEquip)
        {
            return _grupoEquipamentoInterface.ObterPorCodigo(codGrupoEquip);
        }

        [HttpPost]
        public void Post([FromBody] GrupoEquipamento grupoEquipamento)
        {
            int codGrupoEquip = _sequenciaInterface.ObterContador(Constants.TABELA_GRUPO_EQUIPAMENTO);
            grupoEquipamento.CodGrupoEquip = codGrupoEquip;
            _grupoEquipamentoInterface.Criar(grupoEquipamento: grupoEquipamento);
        }

        [HttpPut]
        public void Put([FromBody] GrupoEquipamento grupoEquipamento)
        {
            _grupoEquipamentoInterface.Atualizar(grupoEquipamento);
        }

        [HttpDelete("{codGrupoEquip}")]
        public void Delete(int codGrupoEquip)
        {
            _grupoEquipamentoInterface.Deletar(codGrupoEquip);
        }
    }
}
