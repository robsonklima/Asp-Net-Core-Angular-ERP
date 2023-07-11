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
    public class DespesaCartaoCombustivelController : ControllerBase
    {
        public IDespesaCartaoCombustivelService _despesaCartaoCombustivelService { get; }

        public DespesaCartaoCombustivelController(IDespesaCartaoCombustivelService svc)
        {
            _despesaCartaoCombustivelService = svc;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaCartaoCombustivelParameters parameters) =>
            _despesaCartaoCombustivelService.ObterPorParametros(parameters);

        [HttpGet("{codDespesaCartaoCombustivel}")]
        public DespesaCartaoCombustivel Get(int codDespesaCartaoCombustivel) =>
             _despesaCartaoCombustivelService.ObterPorCodigo(codDespesaCartaoCombustivel);

        [HttpPost]
        public void Post([FromBody] DespesaCartaoCombustivel despesa) =>
           _despesaCartaoCombustivelService.Criar(despesa);

        [HttpPut]
        public void Put([FromBody] DespesaCartaoCombustivel despesa) =>
            _despesaCartaoCombustivelService.Atualizar(despesa);

    }
}