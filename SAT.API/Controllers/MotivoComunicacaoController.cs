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
    public class MotivoComunicacaoController : ControllerBase
    {
        private IMotivoComunicacaoService _MotivoComunicacaoService;

        public MotivoComunicacaoController(
            IMotivoComunicacaoService MotivoComunicacaoService
        )
        {
            _MotivoComunicacaoService = MotivoComunicacaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] MotivoComunicacaoParameters parameters)
        {
            return _MotivoComunicacaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codMotivoComunicacao}")]
        public MotivoComunicacao Get(int codMotivoComunicacao)
        {
            return _MotivoComunicacaoService.ObterPorCodigo(codMotivoComunicacao);
        }

        [HttpPost]
        public MotivoComunicacao Post([FromBody] MotivoComunicacao op)
        {
            return _MotivoComunicacaoService.Criar(op);
        }

        [HttpPut]
        public MotivoComunicacao Put([FromBody] MotivoComunicacao op)
        {
            return _MotivoComunicacaoService.Atualizar(op);
        }

        [HttpDelete("{codMotivoComunicacao}")]
        public MotivoComunicacao Delete(int codMotivoComunicacao)
        {
            return _MotivoComunicacaoService.Deletar(codMotivoComunicacao);
        }
    }
}
