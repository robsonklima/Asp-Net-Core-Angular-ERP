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
    public class MRPLogixEstoqueController : ControllerBase
    {
        private readonly IMRPLogixEstoqueService _mrpLogixService;

        public MRPLogixEstoqueController(IMRPLogixEstoqueService mrpLogixService)
        {
            _mrpLogixService = mrpLogixService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] MRPLogixEstoqueParameters parameters)
        {
            return _mrpLogixService.ObterPorParametros(parameters);
        }

        [HttpGet("{codMRPLogixEstoque}")]
        public MRPLogixEstoque Get(int codMRPLogixEstoque)
        {
            return _mrpLogixService.ObterPorCodigo(codMRPLogixEstoque);
        }

        [HttpPost]
        public void Post([FromBody] MRPLogixEstoque mrpLogix)
        {
            _mrpLogixService.Criar(mrpLogix);
        }

        [HttpPut]
        public void Put([FromBody] MRPLogixEstoque mrpLogix)
        {
            _mrpLogixService.Atualizar(mrpLogix);
        }

        [HttpDelete("{codMRPLogixEstoque}")]
        public void Delete(int codMRPLogixEstoque)
        {
            _mrpLogixService.Deletar(codMRPLogixEstoque);
        }
    }
}