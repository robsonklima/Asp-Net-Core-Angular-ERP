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
    public class TecnicoController : ControllerBase
    {
        private readonly ITecnicoService _tecnicoService;

        public TecnicoController(ITecnicoService tecnicoService)
        {
            this._tecnicoService = tecnicoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TecnicoParameters parameters)
        {
            return _tecnicoService.ObterPorParametros(parameters);
        }

        [HttpGet("Deslocamentos")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel GetDeslocamentos([FromQuery] TecnicoParameters parameters)
        {
            return _tecnicoService.ObterDeslocamentos(parameters);
        }

        [HttpGet("{codTecnico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Tecnico Get(int codTecnico)
        {
            return _tecnicoService.ObterPorCodigo(codTecnico);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] Tecnico tecnico)
        {
            _tecnicoService.Criar(tecnico);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Tecnico tecnico)
        {
            _tecnicoService.Atualizar(tecnico);
        }

        [HttpDelete("{codTecnico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codTecnico)
        {
            _tecnicoService.Deletar(codTecnico);
        }
    }
}
