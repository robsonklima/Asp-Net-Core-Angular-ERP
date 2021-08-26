using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class FeriadoController : ControllerBase
    {
        private readonly IFeriadoService _feriadoService;

        public FeriadoController(IFeriadoService feriadoService)
        {
            _feriadoService = feriadoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] FeriadoParameters parameters)
        {
            return _feriadoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codFeriado}")]
        public Feriado Get(int codFeriado)
        {
            return _feriadoService.ObterPorCodigo(codFeriado);
        }

        [HttpPost]
        public void Post([FromBody] Feriado feriado)
        {
            _feriadoService.Criar(feriado);
        }

        [HttpPut]
        public void Put([FromBody] Feriado feriado)
        {
            _feriadoService.Atualizar(feriado);
        }

        [HttpDelete("{codFeriado}")]
        public void Delete(int codFeriado)
        {
            _feriadoService.Deletar(codFeriado);
        }
    }
}
