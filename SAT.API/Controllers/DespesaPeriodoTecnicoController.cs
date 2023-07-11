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
    public class DespesaPeriodoTecnicoController : ControllerBase
    {
        private readonly IDespesaPeriodoTecnicoService _despesaPeriodoTecnico;

        public DespesaPeriodoTecnicoController(IDespesaPeriodoTecnicoService despesaPeriodoTecnico) =>
            _despesaPeriodoTecnico = despesaPeriodoTecnico;

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaPeriodoTecnicoParameters parameters) =>
            _despesaPeriodoTecnico.ObterPorParametros(parameters);

        [HttpGet("Atendimentos")]
        public ListViewModel GetAtendimentos([FromQuery] DespesaPeriodoTecnicoParameters parameters) =>
            _despesaPeriodoTecnico.ObterAtendimentos(parameters);

        [HttpGet("{codDespesaPeriodoTecnico}")]
        public DespesaPeriodoTecnico Get(int codDespesaPeriodoTecnico) =>
             _despesaPeriodoTecnico.ObterPorCodigo(codDespesaPeriodoTecnico);

        [HttpPost]
        public void Post([FromBody] DespesaPeriodoTecnico despesaPeriodoTecnico) =>
            _despesaPeriodoTecnico.Criar(despesaPeriodoTecnico);

        [HttpPut]
        public DespesaPeriodoTecnico Put([FromBody] DespesaPeriodoTecnico despesaPeriodoTecnico) =>
            _despesaPeriodoTecnico.Atualizar(despesaPeriodoTecnico);

        [HttpDelete("{codDespesaPeriodoTecnico}")]
        public void Delete(int codDespesaPeriodoTecnico) =>
            _despesaPeriodoTecnico.Deletar(codDespesaPeriodoTecnico);
    }
}