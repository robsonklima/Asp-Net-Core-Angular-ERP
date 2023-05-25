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
    public class MRPLogixController : ControllerBase
    {
        private readonly IMRPLogixService _mrpLogixService;

        public MRPLogixController(IMRPLogixService mrpLogixService)
        {
            _mrpLogixService = mrpLogixService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] MRPLogixParameters parameters)
        {
            return _mrpLogixService.ObterPorParametros(parameters);
        }

        [HttpGet("{codMRPLogix}")]
        public MRPLogix Get(int codMRPLogix)
        {
            return _mrpLogixService.ObterPorCodigo(codMRPLogix);
        }

        [HttpPost]
        public void Post([FromBody] MRPLogix mrpLogix)
        {
            _mrpLogixService.Criar(mrpLogix);
        }

        [HttpPut]
        public void Put([FromBody] MRPLogix mrpLogix)
        {
            _mrpLogixService.Atualizar(mrpLogix);
        }

        [HttpDelete("{codMRPLogix}")]
        public void Delete(int codMRPLogix)
        {
            _mrpLogixService.Deletar(codMRPLogix);
        }
    }
}