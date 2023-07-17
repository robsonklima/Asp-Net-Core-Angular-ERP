using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class PontoUsuarioDataDivergenciaController : ControllerBase
    {
        private readonly IPontoUsuarioDataDivergenciaService _pontoUsuarioDataDivergenciaService;

        public PontoUsuarioDataDivergenciaController(IPontoUsuarioDataDivergenciaService pontoUsuarioDataDivergenciaService)
        {
            _pontoUsuarioDataDivergenciaService = pontoUsuarioDataDivergenciaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PontoUsuarioDataDivergenciaParameters parameters)
        {
            return _pontoUsuarioDataDivergenciaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoUsuarioDataDivergencia}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PontoUsuarioDataDivergencia Get(int codPontoUsuarioDataDivergencia)
        {
            return _pontoUsuarioDataDivergenciaService.ObterPorCodigo(codPontoUsuarioDataDivergencia);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia)
        {
            _pontoUsuarioDataDivergenciaService.Criar(pontoUsuarioDataDivergencia: pontoUsuarioDataDivergencia);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia)
        {
            _pontoUsuarioDataDivergenciaService.Atualizar(pontoUsuarioDataDivergencia: pontoUsuarioDataDivergencia);
        }

        [HttpDelete("{codPontoUsuarioDataDivergencia}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPontoUsuarioDataDivergencia)
        {
            _pontoUsuarioDataDivergenciaService.Deletar(codPontoUsuarioDataDivergencia);
        }
    }
}
