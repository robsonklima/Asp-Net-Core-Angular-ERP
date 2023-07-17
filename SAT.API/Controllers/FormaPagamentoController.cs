using System.Security.Claims;
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
    public class FormaPagamentoController : ControllerBase
    {
        private readonly IFormaPagamentoService _formaPagamentoService;

        public FormaPagamentoController(IFormaPagamentoService formaPagamentoService)
        {
            _formaPagamentoService = formaPagamentoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] FormaPagamentoParameters parameters)
        {
            return _formaPagamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codFormaPagamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public FormaPagamento Get(int codFormaPagamento)
        {
            return _formaPagamentoService.ObterPorCodigo(codFormaPagamento);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public FormaPagamento Post([FromBody] FormaPagamento FormaPagamento)
        {
            return _formaPagamentoService.Criar(FormaPagamento);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] FormaPagamento FormaPagamento)
        {
            _formaPagamentoService.Atualizar(FormaPagamento);
        }

        [HttpDelete("{codFormaPagamento}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codFormaPagamento)
        {
            _formaPagamentoService.Deletar(codFormaPagamento);
        }
    }
}
