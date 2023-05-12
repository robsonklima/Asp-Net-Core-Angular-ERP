using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class RedeBanrisulController : ControllerBase
    {
        private IRedeBanrisulService _RedeBanrisulService;

        public RedeBanrisulController(
            IRedeBanrisulService RedeBanrisulService
        )
        {
            _RedeBanrisulService = RedeBanrisulService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] RedeBanrisulParameters parameters)
        {
            return _RedeBanrisulService.ObterPorParametros(parameters);
        }

        [HttpGet("{codRedeBanrisul}")]
        public RedeBanrisul Get(int codRedeBanrisul)
        {
            return _RedeBanrisulService.ObterPorCodigo(codRedeBanrisul);
        }

        [HttpPost]
        public RedeBanrisul Post([FromBody] RedeBanrisul rede)
        {
            return _RedeBanrisulService.Criar(rede);
        }

        [HttpPut]
        public RedeBanrisul Put([FromBody] RedeBanrisul RedeBanrisul)
        {
            return _RedeBanrisulService.Atualizar(RedeBanrisul);
        }

        [HttpDelete("{codRedeBanrisul}")]
        public RedeBanrisul Delete(int codRedeBanrisul)
        {
            return _RedeBanrisulService.Deletar(codRedeBanrisul);
        }
    }
}
