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
    public class DespesaItemController : ControllerBase
    {
        private readonly IDespesaItemService _despesaItemService;

        public DespesaItemController(IDespesaItemService despesaItemService) =>
            _despesaItemService = despesaItemService;

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DespesaItemParameters parameters) {
            return _despesaItemService.ObterPorParametros(parameters);
        }

        [HttpGet("{codDespesaItem}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public DespesaItem Get(int codDespesaItem) =>
             _despesaItemService.ObterPorCodigo(codDespesaItem);

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] DespesaItem despesaItem) =>
            _despesaItemService.Criar(despesaItem);

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] DespesaItem despesaItem) =>
            _despesaItemService.Atualizar(despesaItem);

        [HttpDelete("{codDespesaItem}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codDespesaItem) =>
            _despesaItemService.Deletar(codDespesaItem);
    }
}