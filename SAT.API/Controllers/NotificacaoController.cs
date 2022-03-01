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
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoService _notificacaoService;

        public NotificacaoController(INotificacaoService notificacaoService)
        {
            _notificacaoService = notificacaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] NotificacaoParameters parameters)
        {
            return _notificacaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codNotificacao}")]
        public Notificacao Get(int codNotificacao)
        {
            return _notificacaoService.ObterPorCodigo(codNotificacao);
        }

        [HttpPost]
        public void Post([FromBody] Notificacao notificacao)
        {
            _notificacaoService.Criar(notificacao);
        }

        [HttpPut]
        public void Put([FromBody] Notificacao notificacao)
        {
            _notificacaoService.Atualizar(notificacao);
        }

        [HttpDelete("{codNotificacao}")]
        public void Delete(int codNotificacao)
        {
            _notificacaoService.Deletar(codNotificacao);
        }
    }
}
