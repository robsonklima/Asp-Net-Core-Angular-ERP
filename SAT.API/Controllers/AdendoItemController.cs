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
    public class AdendoItemController : ControllerBase
    {
        private IAdendoItemService _AdendoItemService;

        public AdendoItemController(IAdendoItemService AdendoItemService)
        {
            _AdendoItemService = AdendoItemService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AdendoItemParameters parameters)
        {
            return _AdendoItemService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodAdendoItem}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public AdendoItem Get(int CodAdendoItem)
        {
            return _AdendoItemService.ObterPorCodigo(CodAdendoItem);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public AdendoItem Post([FromBody] AdendoItem item)
        {
            return _AdendoItemService.Criar(item);
        }

        [HttpPut("{codAdendoItem}")]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public AdendoItem Put(int codAdendoItem, [FromBody] AdendoItem item)
        {
            return _AdendoItemService.Atualizar(item);
        }

        [HttpDelete("{CodAdendoItem}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public AdendoItem Delete(int CodAdendoItem)
        {
            return _AdendoItemService.Deletar(CodAdendoItem);
        }
    }
}
