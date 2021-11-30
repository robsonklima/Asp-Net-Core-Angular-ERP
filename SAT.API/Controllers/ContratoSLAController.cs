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
    public class ContratoSLAController : ControllerBase
    {
        public IContratoSLAService _contratoSLAService { get; }

        public ContratoSLAController(IContratoSLAService contratoSLAService)
        {
            _contratoSLAService = contratoSLAService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ContratoSLAParameters parameters)
        {
            return _contratoSLAService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{codContrato}/{codSLA}")]
        public ContratoSLA Get(int codContrato, int codSLA)
        {
            return _contratoSLAService.ObterPorCodigo(codContrato,codSLA);
        }

        [HttpPost]
        public void Post([FromBody] ContratoSLA contratoSLA)
        {
            _contratoSLAService.Criar(contratoSLA);
        }

        [HttpPut]
        public void Put([FromBody] ContratoSLA contratoSLA)
        {
            _contratoSLAService.Atualizar(contratoSLA);
        }

        [HttpDelete("{codContrato}/{codSLA}")]
        public void Delete(int codContrato,int codSLA)
        {
            _contratoSLAService.Deletar(codContrato, codSLA);
        }
    }
}
