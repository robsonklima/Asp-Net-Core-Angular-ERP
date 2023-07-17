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
    public class CausaImprodutividadeController : ControllerBase
    {
        public ICausaImprodutividadeService _causaImprodutividadeService { get; }

        public CausaImprodutividadeController(ICausaImprodutividadeService causaImprodutividadeService)
        {
            _causaImprodutividadeService = causaImprodutividadeService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] CausaImprodutividadeParameters parameters)
        {
            return _causaImprodutividadeService.ObterPorParametros(parameters);
        }
       
        [HttpGet("{CodCausaImprodutividade}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public CausaImprodutividade Get(int codCausaImprodutividade)
        {
            return _causaImprodutividadeService.ObterPorCodigo(codCausaImprodutividade);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] CausaImprodutividade causaImprodutividade)
        {
            _causaImprodutividadeService.Criar(causaImprodutividade);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] CausaImprodutividade causaImprodutividade)
        {
            _causaImprodutividadeService.Atualizar(causaImprodutividade);
        }

        [HttpDelete("{CodCausaImprodutividade}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codCausaImprodutividade)
        {
            _causaImprodutividadeService.Deletar(codCausaImprodutividade);
        }
    }
}
