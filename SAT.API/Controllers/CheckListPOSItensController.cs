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
    public class CheckListPOSItensController : ControllerBase
    {
        public ICheckListPOSItensService _checkListPOSItensService { get; }

        public CheckListPOSItensController(ICheckListPOSItensService checkListPOSItensService)
        {
            _checkListPOSItensService = checkListPOSItensService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] CheckListPOSItensParameters parameters)
        {
            return _checkListPOSItensService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{CodCheckListPOSItens}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public CheckListPOSItens Get(int codCheckListPOSItens)
        {
            return _checkListPOSItensService.ObterPorCodigo(codCheckListPOSItens);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] CheckListPOSItens checkListPOSItens)
        {
            _checkListPOSItensService.Criar(checkListPOSItens);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] CheckListPOSItens checkListPOSItens)
        {
            _checkListPOSItensService.Atualizar(checkListPOSItens);
        }

        [HttpDelete("{CodCheckListPOSItens}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codCheckListPOSItens)
        {
            _checkListPOSItensService.Deletar(codCheckListPOSItens);
        }
    }
}
