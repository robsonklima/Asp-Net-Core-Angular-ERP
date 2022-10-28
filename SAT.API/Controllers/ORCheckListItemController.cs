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
        public ListViewModel Get([FromQuery] ORCheckListItemParameters parameters)
        {
            return _orCheckListItemService.ObterPorParametros(parameters);
        }

        [HttpGet("{cod}")]
        public ORCheckListItem Get(int cod)
        {
            return _orCheckListItemService.ObterPorCodigo(cod);
        }

        [HttpPost]
        public void Post([FromBody] ORCheckListItem item)
        {
            _orCheckListItemService.Criar(item);
        }

        [HttpPut]
        public void Put([FromBody] ORCheckListItem item)
        {
            _orCheckListItemService.Atualizar(item);
        }

        [HttpDelete("{cod}")]
        public void Delete(int cod)
        {
            _orCheckListItemService.Deletar(cod);
        }
    }
}
