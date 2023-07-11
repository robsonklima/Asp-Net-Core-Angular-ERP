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
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class ArquivoBanrisulController : ControllerBase
    {
        public IArquivoBanrisulService _arquivoBanrisulService { get; }

        public ArquivoBanrisulController(IArquivoBanrisulService arquivoBanrisulService)
        {
            _arquivoBanrisulService = arquivoBanrisulService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ArquivoBanrisulParameters parameters)
        {
            return _arquivoBanrisulService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{CodArquivoBanrisul}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ArquivoBanrisul Get(int codArquivoBanrisul)
        {
            return _arquivoBanrisulService.ObterPorCodigo(codArquivoBanrisul);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ArquivoBanrisul ArquivoBanrisul)
        {
            _arquivoBanrisulService.Criar(ArquivoBanrisul);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ArquivoBanrisul ArquivoBanrisul)
        {
            _arquivoBanrisulService.Atualizar(ArquivoBanrisul);
        }

        [HttpDelete("{CodArquivoBanrisul}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codArquivoBanrisul)
        {
            _arquivoBanrisulService.Deletar(codArquivoBanrisul);
        }
    }
}
