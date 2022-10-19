using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemController : ControllerBase
    {
        private readonly IMensagemService _mensagemService;

        public MensagemController(IMensagemService MensagemService)
        {
            _mensagemService = MensagemService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] MensagemParameters parameters)
        {
            return _mensagemService.ObterPorParametros(parameters);
        }

        [HttpGet("{codMsg}")]
        public Mensagem Get(int codMsg)
        {
            return _mensagemService.ObterPorCodigo(codMsg);
        }

        [HttpPost]
        public void Post([FromBody] Mensagem mensagem)
        {
            _mensagemService.Criar(mensagem);
        }

        [HttpPut]
        public void Put([FromBody] Mensagem mensagem)
        {
            _mensagemService.Atualizar(mensagem);
        }

        [HttpDelete("{codMsg}")]
        public void Delete(int codMsg)
        {
            _mensagemService.Deletar(codMsg);
        }
    }
}
