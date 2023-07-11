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
    public class SetorController : ControllerBase
    {
        private readonly ISetorService _setorService;

        public SetorController(ISetorService setorService)
        {
            _setorService = setorService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] SetorParameters parameters)
        {
            return _setorService.ObterPorParametros(parameters);
        }

        [HttpGet("{codSetor}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Setor Get(int codSetor)
        {
            return _setorService.ObterPorCodigo(codSetor);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] Setor setor)
        {
            _setorService.Criar(setor: setor);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Setor setor)
        {
            _setorService.Atualizar(setor: setor);
        }

        [HttpDelete("{codSetor}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codSetor)
        {
            _setorService.Deletar(codSetor);
        }
    }
}
