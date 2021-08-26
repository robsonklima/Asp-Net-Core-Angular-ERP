using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class DefeitoController : ControllerBase
    {
        private readonly IDefeitoService _defeitoService;

        public DefeitoController(
            IDefeitoService defeitoService
        )
        {
            _defeitoService = defeitoService;
        }

        [HttpGet("{codDefeito}")]
        public Defeito Get(int codDefeito)
        {
            return _defeitoService.ObterPorCodigo(codDefeito);
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] DefeitoParameters parameters)
        {
            return _defeitoService.ObterPorParametros(parameters);
        }

        [HttpPost]
        public Defeito Post([FromBody] Defeito defeito)
        {
            return _defeitoService.Criar(defeito);
        }

        [HttpPut]
        public void Put([FromBody] Defeito defeito)
        {
            _defeitoService.Atualizar(defeito);
        }

        [HttpDelete("{codDefeito}")]
        public void Delete(int codDefeito)
        {
            _defeitoService.Deletar(codDefeito);
        }
    }
}
