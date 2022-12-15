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
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class ProtocoloChamadoSTNController : ControllerBase
    {
        public IProtocoloChamadoSTNService _protocoloChamadoSTNService { get; }

        public ProtocoloChamadoSTNController(IProtocoloChamadoSTNService protocoloChamadoSTNService)
        {
            _protocoloChamadoSTNService = protocoloChamadoSTNService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ProtocoloChamadoSTNParameters parameters)
        {
            return _protocoloChamadoSTNService.ObterPorParametros(parameters);
        }
       
        [HttpGet("{CodProtocoloChamadoSTN}")]
        public ProtocoloChamadoSTN Get(int codProtocoloChamadoSTN)
        {
            return _protocoloChamadoSTNService.ObterPorCodigo(codProtocoloChamadoSTN);
        }

        [HttpPost]
        public void Post([FromBody] ProtocoloChamadoSTN protocoloChamadoSTN)
        {
            _protocoloChamadoSTNService.Criar(protocoloChamadoSTN);
        }

        [HttpPut]
        public void Put([FromBody] ProtocoloChamadoSTN protocoloChamadoSTN)
        {
            _protocoloChamadoSTNService.Atualizar(protocoloChamadoSTN);
        }

        [HttpDelete("{CodProtocoloChamadoSTN}")]
        public void Delete(int codProtocoloChamadoSTN)
        {
            _protocoloChamadoSTNService.Deletar(codProtocoloChamadoSTN);
        }
    }
}
