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
    public class FaturamentoFinanceiroController : ControllerBase
    {
        private readonly IFaturamentoFinanceiroService _faturamentoService;

        public FaturamentoFinanceiroController(IFaturamentoFinanceiroService faturamentoService)
        {
            _faturamentoService = faturamentoService;
        }

        [HttpPost]
        public void Post([FromBody] FaturamentoFinanceiro faturamento)
        {
            this._faturamentoService.Criar(faturamento);
        }
    }
}
