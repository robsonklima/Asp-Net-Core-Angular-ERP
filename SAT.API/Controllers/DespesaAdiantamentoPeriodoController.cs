using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaAdiantamentoPeriodoController : ControllerBase
    {
        private readonly IDespesaAdiantamentoPeriodoService _despesaAdiantamentoPeriodoService;

        public DespesaAdiantamentoPeriodoController(IDespesaAdiantamentoPeriodoService despesaAdiantamentoPeriodoService) =>
            _despesaAdiantamentoPeriodoService = despesaAdiantamentoPeriodoService;

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DespesaAdiantamentoPeriodoParameters parameters) =>
            _despesaAdiantamentoPeriodoService.ObterPorParametros(parameters);

        [HttpGet("Tecnicos")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel GetConsultaTecnico([FromQuery] DespesaAdiantamentoPeriodoParameters parameters) =>
            _despesaAdiantamentoPeriodoService.ObterConsultaTecnicos(parameters);

        [HttpGet("{codDespesaAdiantamentoPeriodo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public DespesaAdiantamentoPeriodo Get(int codDespesaAdiantamentoPeriodo) =>
             _despesaAdiantamentoPeriodoService.ObterPorCodigo(codDespesaAdiantamentoPeriodo);

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] DespesaAdiantamentoPeriodo despesa) =>
            _despesaAdiantamentoPeriodoService.Criar(despesa);

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] DespesaAdiantamentoPeriodo despesa) =>
            _despesaAdiantamentoPeriodoService.Atualizar(despesa);

        [HttpDelete("{codDespesaAdiantamentoPeriodo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codDespesaAdiantamentoPeriodo) =>
            _despesaAdiantamentoPeriodoService.Deletar(codDespesaAdiantamentoPeriodo);
    }
}