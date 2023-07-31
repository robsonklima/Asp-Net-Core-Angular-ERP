using System.Security.Claims;
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
        //[ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] NotificacaoParameters parameters)
        {
            return _notificacaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codNotificacao}")]
        //[ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Notificacao Get(int codNotificacao)
        {
            return _notificacaoService.ObterPorCodigo(codNotificacao);
        }

        [HttpPost]
        //[ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public Notificacao Post([FromBody] Notificacao notificacao)
        {
            return _notificacaoService.Criar(notificacao);
        }

        [HttpPut]
        //[ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Notificacao notificacao)
        {
            _notificacaoService.Atualizar(notificacao);
        }

        [HttpDelete("{codNotificacao}")]
        //[ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codNotificacao)
        {
            _notificacaoService.Deletar(codNotificacao);
        }
    }
}
