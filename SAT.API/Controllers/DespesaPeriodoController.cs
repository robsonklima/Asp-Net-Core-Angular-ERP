using SAT.MODELS.Entities.Params;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Authorization;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaPeriodoController : ControllerBase
    {
        private readonly IDespesaPeriodoService _despesaPeriodo;

        public DespesaPeriodoController(IDespesaPeriodoService despesaPeriodo) =>
            _despesaPeriodo = despesaPeriodo;

        //[CustomAuthorize()]
        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaPeriodoParameters parameters) =>
            _despesaPeriodo.ObterPorParametros(parameters);

        [HttpGet("{codDespesaPeriodo}")]
        public DespesaPeriodo Get(int codDespesaPeriodo) =>
             _despesaPeriodo.ObterPorCodigo(codDespesaPeriodo);

        [HttpPost]
        //[CustomAuthorize(RoleGroup.FINANCEIRO, RoleEnum.FINANCEIRO_COORDENADOR_CREDITO)]
        public void Post([FromBody] DespesaPeriodo despesa) =>
            _despesaPeriodo.Criar(despesa);

        [HttpPut]
        //[CustomAuthorize(RoleGroup.FINANCEIRO, RoleEnum.FINANCEIRO_COORDENADOR_CREDITO)]
        public void Put([FromBody] DespesaPeriodo despesa) =>
            _despesaPeriodo.Atualizar(despesa);

        [HttpDelete("{codDespesaPeriodo}")]
        //[CustomAuthorize(RoleGroup.FINANCEIRO, RoleEnum.FINANCEIRO_COORDENADOR_CREDITO)]
        public void Delete(int codDespesaPeriodo) =>
            _despesaPeriodo.Deletar(codDespesaPeriodo);
    }
}