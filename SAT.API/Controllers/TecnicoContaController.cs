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
    public class TecnicoContaController : ControllerBase
    {
        private readonly ITecnicoContaService _tecnicoContaService;

        public TecnicoContaController(ITecnicoContaService tecnicoContaService)
        {
            _tecnicoContaService = tecnicoContaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TecnicoContaParameters parameters)
        {
            return _tecnicoContaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codContaTecnico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public TecnicoConta Get(int codConta)
        {
            return _tecnicoContaService.ObterPorCodigo(codConta);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] TecnicoConta conta)
        {
            _tecnicoContaService.Criar(conta);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] TecnicoConta conta)
        {
            _tecnicoContaService.Atualizar(conta);
        }

        [HttpDelete("{codTecnico}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codTecnico)
        {
            _tecnicoContaService.Deletar(codTecnico);
        }
    }
}
