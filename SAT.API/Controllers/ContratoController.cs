using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;


namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoRepository _contratoInterface;

        public ContratoController(IContratoRepository contratoInterface)
        {
            _contratoInterface = contratoInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ContratoParameters parameters)
        {
            var contratos = _contratoInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = contratos,
                TotalCount = contratos.TotalCount,
                CurrentPage = contratos.CurrentPage,
                PageSize = contratos.PageSize,
                TotalPages = contratos.TotalPages,
                HasNext = contratos.HasNext,
                HasPrevious = contratos.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codContrato}")]
        public Contrato Get(int codContrato)
        {
            return _contratoInterface.ObterPorCodigo(codContrato);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut]
        public void Put([FromBody] string value)
        {
        }

        [HttpDelete("{codContrato}")]
        public void Delete(int codContrato)
        {
        }
    }
}
