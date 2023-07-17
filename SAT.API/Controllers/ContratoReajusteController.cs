using SAT.MODELS.Entities.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class ContratoReajusteController : ControllerBase
    {
        private readonly IContratoReajusteService _contratoReajusteService;

        public ContratoReajusteController(IContratoReajusteService contratoReajusteService)
        {
            _contratoReajusteService = contratoReajusteService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ContratoReajusteParameters parameters)
        {
            return _contratoReajusteService.ObterPorParametros(parameters);
        }

        [HttpGet("{codContratoReajuste}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ContratoReajuste Get(int codContratoReajuste)
        {
            return _contratoReajusteService.ObterPorCodigo(codContratoReajuste);
        }        

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ContratoReajuste contratoReajuste)
        {
            _contratoReajusteService.Criar(contratoReajuste);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ContratoReajuste contratoReajuste)
        {
            _contratoReajusteService.Atualizar(contratoReajuste);
        }

        [HttpDelete("{codContratoReajuste}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codContratoReajuste)
        {
            _contratoReajusteService.Deletar(codContratoReajuste);
        }
    }
}
