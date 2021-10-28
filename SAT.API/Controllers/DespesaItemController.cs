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
    public class DespesaItemController : ControllerBase
    {
        private readonly IDespesaItemService _despesaItemService;

        public DespesaItemController(IDespesaItemService despesaItemService) =>
            _despesaItemService = despesaItemService;

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaItemParameters parameters) =>
            _despesaItemService.ObterPorParametros(parameters);

        [HttpGet("{codDespesaItem}")]
        public DespesaItem Get(int codDespesaItem) =>
             _despesaItemService.ObterPorCodigo(codDespesaItem);

        [HttpPost]
        public void Post([FromBody] DespesaItem despesaItem) =>
            _despesaItemService.Criar(despesaItem);

        [HttpPut]
        public void Put([FromBody] DespesaItem despesaItem) =>
            _despesaItemService.Atualizar(despesaItem);

        [HttpDelete("{codDespesaItem}")]
        public void Delete(int codDespesaItem) =>
            _despesaItemService.Deletar(codDespesaItem);
    }
}