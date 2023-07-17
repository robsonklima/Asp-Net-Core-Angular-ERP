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
    public class PecaRE5114Controller : ControllerBase
    {
        private readonly IPecaRE5114Service _pecaRE5114Service;

        public PecaRE5114Controller(IPecaRE5114Service pecaRE5114Service)
        {
            _pecaRE5114Service = pecaRE5114Service;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PecaRE5114Parameters parameters)
        {
            return _pecaRE5114Service.ObterPorParametros(parameters);
        }

        [HttpGet("{CodPecaRe5114}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PecaRE5114 Get(int codPecaRE5114)
        {
            return _pecaRE5114Service.ObterPorCodigo(codPecaRE5114);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] PecaRE5114 pecaRE5114)
        {
            _pecaRE5114Service.Criar(pecaRE5114);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PecaRE5114 pecaRE5114)
        {
            _pecaRE5114Service.Atualizar(pecaRE5114);
        }

        [HttpDelete("{CodPecaRe5114}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPecaRE5114)
        {
            _pecaRE5114Service.Deletar(codPecaRE5114);
        }

    }
}