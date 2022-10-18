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
    public class ORItemController : ControllerBase
    {
        private readonly IORItemService _ORItemService;

        public ORItemController(IORItemService ORItemService)
        {
            _ORItemService = ORItemService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ORItemParameters parameters)
        {
            return _ORItemService.ObterPorParametros(parameters);
        }

        [HttpGet("{codORItem}")]
        public ORItem Get(int codORItem)
        {
            return _ORItemService.ObterPorCodigo(codORItem);
        }

        [HttpPost]
        public void Post([FromBody] ORItem item)
        {
            _ORItemService.Criar(item);
        }

        [HttpPut]
        public void Put([FromBody] ORItem item)
        {
            _ORItemService.Atualizar(item);
        }

        [HttpDelete("{codORItem}")]
        public void Delete(int codORItem)
        {
            _ORItemService.Deletar(codORItem);
        }
    }
}
