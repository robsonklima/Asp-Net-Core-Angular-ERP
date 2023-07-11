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
    public class DespesaAdiantamentoPeriodoController : ControllerBase
    {
        private readonly IDespesaAdiantamentoPeriodoService _despesaAdiantamentoPeriodoService;

        public DespesaAdiantamentoPeriodoController(IDespesaAdiantamentoPeriodoService despesaAdiantamentoPeriodoService) =>
            _despesaAdiantamentoPeriodoService = despesaAdiantamentoPeriodoService;

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaAdiantamentoPeriodoParameters parameters) =>
            _despesaAdiantamentoPeriodoService.ObterPorParametros(parameters);

        [HttpGet("Tecnicos")]
        public ListViewModel GetConsultaTecnico([FromQuery] DespesaAdiantamentoPeriodoParameters parameters) =>
            _despesaAdiantamentoPeriodoService.ObterConsultaTecnicos(parameters);

        [HttpGet("{codDespesaAdiantamentoPeriodo}")]
        public DespesaAdiantamentoPeriodo Get(int codDespesaAdiantamentoPeriodo) =>
             _despesaAdiantamentoPeriodoService.ObterPorCodigo(codDespesaAdiantamentoPeriodo);

        [HttpPost]
        public void Post([FromBody] DespesaAdiantamentoPeriodo despesa) =>
            _despesaAdiantamentoPeriodoService.Criar(despesa);

        [HttpPut]
        public void Put([FromBody] DespesaAdiantamentoPeriodo despesa) =>
            _despesaAdiantamentoPeriodoService.Atualizar(despesa);

        [HttpDelete("{codDespesaAdiantamentoPeriodo}")]
        public void Delete(int codDespesaAdiantamentoPeriodo) =>
            _despesaAdiantamentoPeriodoService.Deletar(codDespesaAdiantamentoPeriodo);
    }
}