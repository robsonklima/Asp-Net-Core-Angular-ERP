using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class PontoUsuarioDataController : ControllerBase
    {
        private readonly IPontoUsuarioDataService _pontoUsuarioDataService;

        public PontoUsuarioDataController(IPontoUsuarioDataService pontoUsuarioDataService)
        {
            _pontoUsuarioDataService = pontoUsuarioDataService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PontoUsuarioDataParameters parameters)
        {
            return _pontoUsuarioDataService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoUsuarioData}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PontoUsuarioData Get(int codPontoUsuarioData)
        {
            return _pontoUsuarioDataService.ObterPorCodigo(codPontoUsuarioData);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] PontoUsuarioData pontoUsuarioData)
        {
            _pontoUsuarioDataService.Criar(pontoUsuarioData: pontoUsuarioData);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PontoUsuarioData pontoUsuarioData)
        {
            _pontoUsuarioDataService.Atualizar(pontoUsuarioData: pontoUsuarioData);
        }

        [HttpDelete("{codPontoUsuarioData}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPontoUsuarioData)
        {
            _pontoUsuarioDataService.Deletar(codPontoUsuarioData);
        }
    }
}
