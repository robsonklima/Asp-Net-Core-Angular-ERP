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
    public class ContratoServicoController : ControllerBase
    {
        public IContratoServicoService _contratoServicoService { get; }

        public ContratoServicoController(IContratoServicoService contratoServicoService)
        {
            _contratoServicoService = contratoServicoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ContratoServicoParameters parameters)
        {
            return _contratoServicoService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{codContrato}/{codContratoServico}")]
        public ContratoServico Get(int codContrato, int codContratoServico)
        {
            return _contratoServicoService.ObterPorCodigo(codContrato,codContratoServico);
        }

        [HttpPost]
        public void Post([FromBody] ContratoServico contratoServico)
        {
            _contratoServicoService.Criar(contratoServico);
        }

        [HttpPut]
        public void Put([FromBody] ContratoServico contratoServico)
        {
            _contratoServicoService.Atualizar(contratoServico);
        }

        [HttpDelete("{codContrato}/{codContratoServico}")]
        public void Delete(int codContrato,int codContratoServico)
        {
            _contratoServicoService.Deletar(codContrato, codContratoServico);
        }
    }
}
