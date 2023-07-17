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
    public class ORCheckListController : ControllerBase
    {
        private readonly IORCheckListService _orCheckListService;

        public ORCheckListController(IORCheckListService ORCheckListService)
        {
            _orCheckListService = ORCheckListService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ORCheckListParameters parameters)
        {
            return _orCheckListService.ObterPorParametros(parameters);
        }

        [HttpGet("{cod}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ORCheckList Get(int cod)
        {
            return _orCheckListService.ObterPorCodigo(cod);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ORCheckList checkList)
        {
            _orCheckListService.Criar(checkList);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ORCheckList checkList)
        {
            _orCheckListService.Atualizar(checkList);
        }

        [HttpDelete("{cod}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codORCheckList)
        {
            _orCheckListService.Deletar(codORCheckList);
        }
    }
}
