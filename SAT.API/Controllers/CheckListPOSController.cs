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
    public class CheckListPOSController : ControllerBase
    {
        public ICheckListPOSService _checkListPOSService { get; }

        public CheckListPOSController(ICheckListPOSService checkListPOSService)
        {
            _checkListPOSService = checkListPOSService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] CheckListPOSParameters parameters)
        {
            return _checkListPOSService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{CodCheckListPOS}")]
        public CheckListPOS Get(int codCheckListPOS)
        {
            return _checkListPOSService.ObterPorCodigo(codCheckListPOS);
        }

        [HttpPost]
        public void Post([FromBody] CheckListPOS checkListPOS)
        {
            _checkListPOSService.Criar(checkListPOS);
        }

        [HttpPut]
        public void Put([FromBody] CheckListPOS checkListPOS)
        {
            _checkListPOSService.Atualizar(checkListPOS);
        }

        [HttpDelete("{CodCheckListPOS}")]
        public void Delete(int codCheckListPOS)
        {
            _checkListPOSService.Deletar(codCheckListPOS);
        }
    }
}
