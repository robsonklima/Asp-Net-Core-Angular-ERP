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
    public class PerfilSetorController : ControllerBase
    {
        private readonly IPerfilSetorService _perfilSetorService;

        public PerfilSetorController(IPerfilSetorService perfilSetorService)
        {
            _perfilSetorService = perfilSetorService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PerfilSetorParameters parameters)
        {
            return _perfilSetorService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPerfilSetor}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PerfilSetor Get(int codPerfilSetor)
        {
            return _perfilSetorService.ObterPorCodigo(codPerfilSetor);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] PerfilSetor perfilSetor)
        {
            _perfilSetorService.Criar(perfilSetor: perfilSetor);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PerfilSetor perfilSetor)
        {
            _perfilSetorService.Atualizar(perfilSetor: perfilSetor);
        }

        [HttpDelete("{codPerfilSetor}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPerfilSetor)
        {
            _perfilSetorService.Deletar(codPerfilSetor);
        }
    }
}
