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
    public class OSBancadaPecasController : ControllerBase
    {
        private readonly IOSBancadaPecasService _OSBancadaPecasService;

        public OSBancadaPecasController(IOSBancadaPecasService OSBancadaPecasService)
        {
            _OSBancadaPecasService = OSBancadaPecasService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] OSBancadaPecasParameters parameters)
        {
            return _OSBancadaPecasService.ObterPorParametros(parameters);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public OSBancadaPecas Post([FromBody] OSBancadaPecas OSBancadaPecas)
        {
            return _OSBancadaPecasService.Criar(OSBancadaPecas);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] OSBancadaPecas OSBancadaPecas)
        {
            _OSBancadaPecasService.Atualizar(OSBancadaPecas);
        }

        [HttpDelete("{CodOsbancada}/{CodPecaRe5114}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codOsbancada, int codPecaRe5114)
        {
            _OSBancadaPecasService.Deletar(codOsbancada,codPecaRe5114);
        }
    }
}
