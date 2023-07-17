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
    public class ORCheckListItemController : ControllerBase
    {
        private readonly IORCheckListItemService _orCheckListItemService;

        public ORCheckListItemController(IORCheckListItemService orCheckListItemService)
        {
            _orCheckListItemService = orCheckListItemService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ORCheckListItemParameters parameters)
        {
            return _orCheckListItemService.ObterPorParametros(parameters);
        }

        [HttpGet("{cod}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ORCheckListItem Get(int cod)
        {
            return _orCheckListItemService.ObterPorCodigo(cod);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ORCheckListItem item)
        {
            _orCheckListItemService.Criar(item);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ORCheckListItem item)
        {
            _orCheckListItemService.Atualizar(item);
        }

        [HttpDelete("{cod}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int cod)
        {
            _orCheckListItemService.Deletar(cod);
        }
    }
}
