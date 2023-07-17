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
    public class DespesaProtocoloController : ControllerBase
    {
        private readonly IDespesaProtocoloService _protocoloService;

        public DespesaProtocoloController(IDespesaProtocoloService protocoloService) =>
            _protocoloService = protocoloService;

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DespesaProtocoloParameters parameters) =>
            _protocoloService.ObterPorParametros(parameters);

        [HttpGet("{codDespesaProtocolo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public DespesaProtocolo Get(int codDespesaProtocolo) =>
             _protocoloService.ObterPorCodigo(codDespesaProtocolo);

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] DespesaProtocolo despesaProtocolo) =>
           _protocoloService.Atualizar(despesaProtocolo);

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] DespesaProtocolo despesaProtocolo)
        {
            _protocoloService.Criar(despesaProtocolo);
        }

        [HttpDelete("{codDespesaProtocolo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codDespesaProtocolo)
        {
            _protocoloService.Deletar(codDespesaProtocolo);
        }
    }
}