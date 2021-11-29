using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class SLAController : ControllerBase
    {
        private readonly ISLAService _SLAInterface;

        public SLAController(ISLAService slaInterface)
        {
            _SLAInterface = slaInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] SLAParameters parameters)
        {
            return _SLAInterface.ObterPorParametros(parameters);
        }

        [HttpGet("{codSLA}")]
        public SLA Get(int codSLA)
        {
            return _SLAInterface.ObterPorCodigo(codSLA);
        }

        [HttpPost]
        public SLA Post([FromBody] SLA contrato)
        {
            return _SLAInterface.Criar(contrato);
        }

        [HttpPut]
        public SLA Put([FromBody] SLA contrato)
        {
            return _SLAInterface.Atualizar(contrato);
        }

        [HttpDelete("{codSLA}")]
        public void Delete(int codSLA)
        {
            throw new System.NotImplementedException("DELETAR NÃO IMPLEMENTADO");
        }
    }
}
