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
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;

        public CidadeController(ICidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] CidadeParameters parameters)
        {
            return _cidadeService.ObterPorParametros(parameters);
        }

        [HttpGet("{codCidade}")]
        public Cidade Get(int codCidade)
        {
            return _cidadeService.ObterPorCodigo(codCidade);
        }

        [HttpPost]
        public Cidade Post([FromBody] Cidade cidade)
        {
            return _cidadeService.Criar(cidade);
        }

        [HttpPut]
        public void Put([FromBody] Cidade cidade)
        {
            _cidadeService.Atualizar(cidade);
        }

        [HttpDelete("{codCidade}")]
        public void Delete(int codCidade)
        {
            _cidadeService.Deletar(codCidade);
        }
    }
}
