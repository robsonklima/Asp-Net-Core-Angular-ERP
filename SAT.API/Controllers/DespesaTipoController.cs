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
    public class DespesaTipoController : ControllerBase
    {
        private readonly IDespesaTipoService _despesaTipoService;

        public DespesaTipoController(IDespesaTipoService despesaTipoService) =>
            _despesaTipoService = despesaTipoService;

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaTipoParameters parameters) =>
            _despesaTipoService.ObterPorParametros(parameters);

        [HttpGet("{codDespesaTipo}")]
        public DespesaTipo Get(int codDespesaTipo) =>
             _despesaTipoService.ObterPorCodigo(codDespesaTipo);

        [HttpPost]
        public void Post([FromBody] DespesaTipo despesaTipo) =>
            _despesaTipoService.Criar(despesaTipo);

        [HttpPut]
        public void Put([FromBody] DespesaTipo despesaTipo) =>
            _despesaTipoService.Atualizar(despesaTipo);

        [HttpDelete("{codDespesaTipo}")]
        public void Delete(int codDespesaTipo) =>
            _despesaTipoService.Deletar(codDespesaTipo);
    }
}