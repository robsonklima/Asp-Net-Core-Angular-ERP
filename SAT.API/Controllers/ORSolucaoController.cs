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
    public class ORSolucaoController : ControllerBase
    {
        private readonly IORSolucaoService _orSolucaoService;

        public ORSolucaoController(IORSolucaoService orSolucaoService)
        {
            _orSolucaoService = orSolucaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ORSolucaoParameters parameters)
        {
            return _orSolucaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codSolucao}")]
        public ORSolucao Get(int codSolucao)
        {
            return _orSolucaoService.ObterPorCodigo(codSolucao);
        }

        [HttpPost]
        public void Post([FromBody] ORSolucao orSolucao)
        {
            _orSolucaoService.Criar(orSolucao);
        }

        [HttpPut]
        public void Put([FromBody] ORSolucao orSolucao)
        {
            _orSolucaoService.Atualizar(orSolucao);
        }

        [HttpDelete("{codSolucao}")]
        public void Delete(int codSolucao)
        {
            _orSolucaoService.Deletar(codSolucao);
        }
    }
}
