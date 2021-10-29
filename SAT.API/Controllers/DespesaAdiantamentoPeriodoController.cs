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
    public class DespesaAdiantamentoPeriodoController : ControllerBase
    {
        private readonly IDespesaAdiantamentoPeriodoService _despesaAdiantamentoPeriodoService;

        public DespesaAdiantamentoPeriodoController(IDespesaAdiantamentoPeriodoService despesaAdiantamentoPeriodoService) =>
            _despesaAdiantamentoPeriodoService = despesaAdiantamentoPeriodoService;

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaAdiantamentoPeriodoParameters parameters) =>
            _despesaAdiantamentoPeriodoService.ObterPorParametros(parameters);

        [HttpGet("{codDespesaAdiantamentoPerioo}")]
        public DespesaAdiantamentoPeriodo Get(int codDespesaAdiantamentoPerioo) =>
             _despesaAdiantamentoPeriodoService.ObterPorCodigo(codDespesaAdiantamentoPerioo);

        [HttpPost]
        public void Post([FromBody] DespesaAdiantamentoPeriodo despesa) =>
            _despesaAdiantamentoPeriodoService.Criar(despesa);

        [HttpPut]
        public void Put([FromBody] DespesaAdiantamentoPeriodo despesa) =>
            _despesaAdiantamentoPeriodoService.Atualizar(despesa);

        [HttpDelete("{codDespesaAdiantamentoPerioo}")]
        public void Delete(int codDespesaAdiantamentoPerioo) =>
            _despesaAdiantamentoPeriodoService.Deletar(codDespesaAdiantamentoPerioo);
    }
}