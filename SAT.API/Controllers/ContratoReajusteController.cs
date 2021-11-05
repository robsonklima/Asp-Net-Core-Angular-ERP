using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class ContratoReajusteController : ControllerBase
    {
        private readonly IContratoReajusteService _contratoReajusteService;

        public ContratoReajusteController(IContratoReajusteService contratoReajusteService)
        {
            _contratoReajusteService = contratoReajusteService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ContratoReajusteParameters parameters)
        {
            return _contratoReajusteService.ObterPorParametros(parameters);
        }

        [HttpGet("{codContratoReajuste}")]
        public ContratoReajuste Get(int codContratoReajuste)
        {
            return _contratoReajusteService.ObterPorCodigo(codContratoReajuste);
        }        

        [HttpPost]
        public void Post([FromBody] ContratoReajuste contratoReajuste)
        {
            _contratoReajusteService.Criar(contratoReajuste);
        }

        [HttpPut]
        public void Put([FromBody] ContratoReajuste contratoReajuste)
        {
            _contratoReajusteService.Atualizar(contratoReajuste);
        }

        [HttpDelete("{codContratoReajuste}")]
        public void Delete(int codContratoReajuste)
        {
            _contratoReajusteService.Deletar(codContratoReajuste);
        }
    }
}
