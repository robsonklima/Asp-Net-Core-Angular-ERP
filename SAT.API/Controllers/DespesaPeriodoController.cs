using SAT.MODELS.Entities.Params;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaPeriodoController : ControllerBase
    {
        private readonly IDespesaPeriodoService _despesaPeriodo;

        public DespesaPeriodoController(IDespesaPeriodoService despesaPeriodo) =>
            _despesaPeriodo = despesaPeriodo;

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaPeriodoParameters parameters) =>
            _despesaPeriodo.ObterPorParametros(parameters);

        [HttpGet("{codDespesaPeriodo}")]
        public DespesaPeriodo Get(int codDespesaPeriodo) =>
             _despesaPeriodo.ObterPorCodigo(codDespesaPeriodo);

        [HttpPost]
        public void Post([FromBody] DespesaPeriodo despesa) =>
            _despesaPeriodo.Criar(despesa);

        [HttpPut]
        public void Put([FromBody] DespesaPeriodo despesa) =>
            _despesaPeriodo.Atualizar(despesa);

        [HttpDelete("{codDespesaPeriodo}")]
        public void Delete(int codDespesaPeriodo) =>
            _despesaPeriodo.Deletar(codDespesaPeriodo);
    }
}