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
    public class DespesaConfiguracaoCombustivelController : ControllerBase
    {
        private readonly IDespesaConfiguracaoCombustivelService _despesaConfiguracaoCombustivelService;

        public DespesaConfiguracaoCombustivelController(IDespesaConfiguracaoCombustivelService despesaConfiguracaoCombustivelService)
        {
            _despesaConfiguracaoCombustivelService = despesaConfiguracaoCombustivelService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DespesaConfiguracaoCombustivelParameters parameters)
        {

            return _despesaConfiguracaoCombustivelService.ObterPorParametros(parameters);
        
        }

        [HttpGet("{codDespesaConfiguracaoCombustivel}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public DespesaConfiguracaoCombustivel Get(int codDespesaConfiguracaoCombustivel)
        {
             return _despesaConfiguracaoCombustivelService.ObterPorCodigo(codDespesaConfiguracaoCombustivel);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] DespesaConfiguracaoCombustivel despesaConfiguracaoCombustivel)
        {
            _despesaConfiguracaoCombustivelService.Criar(despesaConfiguracaoCombustivel);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] DespesaConfiguracaoCombustivel despesaConfiguracaoCombustivel)
        {
            _despesaConfiguracaoCombustivelService.Atualizar(despesaConfiguracaoCombustivel);
        }

        [HttpDelete("{codDespesaConfiguracaoCombustivel}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codDespesaConfiguracaoCombustivel)
        {
            _despesaConfiguracaoCombustivelService.Deletar(codDespesaConfiguracaoCombustivel);
        }
    }
}