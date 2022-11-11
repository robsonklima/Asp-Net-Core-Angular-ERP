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
    public class ItemXORCheckListController : ControllerBase
    {
        private readonly IItemXORCheckListService _ItemXORCheckListService;

        public ItemXORCheckListController(IItemXORCheckListService ItemXORCheckListService)
        {
            _ItemXORCheckListService = ItemXORCheckListService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ItemXORCheckListParameters parameters)
        {
            return _ItemXORCheckListService.ObterPorParametros(parameters);
        }

        [HttpGet("{codItemXORCheckList}")]
        public ItemXORCheckList Get(int codItemXORCheckList)
        {
            return _ItemXORCheckListService.ObterPorCodigo(codItemXORCheckList);
        }

        [HttpPost]
        public void Post([FromBody] ItemXORCheckList item)
        {
            _ItemXORCheckListService.Criar(item);
        }

        [HttpPut]
        public void Put([FromBody] ItemXORCheckList item)
        {
            _ItemXORCheckListService.Atualizar(item);
        }

        [HttpDelete("{codItemXORCheckList}")]
        public void Delete(int codItemXORCheckList)
        {
            _ItemXORCheckListService.Deletar(codItemXORCheckList);
        }
    }
}