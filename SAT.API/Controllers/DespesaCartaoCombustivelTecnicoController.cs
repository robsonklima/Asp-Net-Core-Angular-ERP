using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Params;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaCartaoCombustivelTecnicoController : ControllerBase
    {
        private readonly IDespesaCartaoCombustivelTecnicoService _cartaoCombustivelSvc;

        public DespesaCartaoCombustivelTecnicoController(IDespesaCartaoCombustivelTecnicoService cartaoCombustivelSvc)
        {
            _cartaoCombustivelSvc = cartaoCombustivelSvc;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DespesaCartaoCombustivelTecnicoParameters parameters)
        {
            return _cartaoCombustivelSvc.ObterPorParametros(parameters);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] DespesaCartaoCombustivelTecnico despesa)
        {
            _cartaoCombustivelSvc.Criar(despesa);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] DespesaCartaoCombustivelTecnico despesa)
        {
            _cartaoCombustivelSvc.Atualizar(despesa);
        }
    }
}
