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
    public class DefeitoComponenteController : ControllerBase
    {
        private readonly IDefeitoComponenteService _defeitoComponenteService;

        public DefeitoComponenteController(IDefeitoComponenteService defeitoComponenteService)
        {
            _defeitoComponenteService = defeitoComponenteService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] DefeitoComponenteParameters parameters)
        {
            return _defeitoComponenteService.ObterPorParametros(parameters);
        }

        [HttpGet("{codDefeitoComponente}")]
        public DefeitoComponente Get(int codDefeitoComponente)
        {
            return _defeitoComponenteService.ObterPorCodigo(codDefeitoComponente);
        }

        [HttpPost]
        public void Post([FromBody] DefeitoComponente defeito)
        {
            _defeitoComponenteService.Criar(defeito);
        }

        [HttpPut]
        public void Put([FromBody] DefeitoComponente defeito)
        {
            _defeitoComponenteService.Atualizar(defeito);
        }

        [HttpDelete("{codDefeitoComponente}")]
        public void Delete(int codDefeito)
        {
            _defeitoComponenteService.Deletar(codDefeito);
        }
    }
}
