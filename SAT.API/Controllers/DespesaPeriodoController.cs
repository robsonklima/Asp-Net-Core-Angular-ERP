using SAT.MODELS.Entities.Params;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DespesaPeriodoParameters parameters) =>
            _despesaPeriodo.ObterPorParametros(parameters);

        [HttpGet("{codDespesaPeriodo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public DespesaPeriodo Get(int codDespesaPeriodo) =>
             _despesaPeriodo.ObterPorCodigo(codDespesaPeriodo);

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] DespesaPeriodo despesa) =>
            _despesaPeriodo.Criar(despesa);

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] DespesaPeriodo despesa) =>
            _despesaPeriodo.Atualizar(despesa);

        [HttpDelete("{codDespesaPeriodo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codDespesaPeriodo) =>
            _despesaPeriodo.Deletar(codDespesaPeriodo);
    }
}