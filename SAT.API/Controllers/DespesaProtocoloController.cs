using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Authorization;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
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

        [CustomAuthorize(RoleGroup.FINANCEIRO, RoleEnum.FILIAL_LIDER, RoleEnum.FILIAL_COORDENADOR, RoleEnum.FILIAIS_SUPERVISOR, RoleEnum.FINANCEIRO_COORDENADOR_CREDITO)]
        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaProtocoloParameters parameters) =>
            _protocoloService.ObterPorParametros(parameters);

        [CustomAuthorize(RoleGroup.FINANCEIRO, RoleEnum.FILIAL_LIDER, RoleEnum.FILIAL_COORDENADOR, RoleEnum.FILIAIS_SUPERVISOR, RoleEnum.FINANCEIRO_COORDENADOR_CREDITO)]
        [HttpGet("{codDespesaProtocolo}")]
        public DespesaProtocolo Get(int codDespesaProtocolo) =>
             _protocoloService.ObterPorCodigo(codDespesaProtocolo);

        [HttpPut]
        public void Put([FromBody] DespesaProtocolo despesaProtocolo) =>
           _protocoloService.Atualizar(despesaProtocolo);

        [HttpPost]
        public void Post([FromBody] DespesaProtocolo despesaProtocolo)
        {
            _protocoloService.Criar(despesaProtocolo);
        }
        
        [HttpDelete("{codDespesaProtocolo}")]
        public void Delete(int codDespesaProtocolo)
        {
            _protocoloService.Deletar(codDespesaProtocolo);
        }
    }
}