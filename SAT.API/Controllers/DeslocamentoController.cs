using System.Threading.Tasks;
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
    public class DeslocamentoController : ControllerBase
    {
        private readonly IDeslocamentoService _deslocamentoService;

        public DeslocamentoController(
            IDeslocamentoService deslocamentoService
        )
        {
            _deslocamentoService = deslocamentoService;
        }

        [HttpGet]
        public async Task<ListViewModel> Get([FromQuery] DeslocamentoParameters parameters) =>
            await _deslocamentoService.ObterPorParametrosAsync(parameters);
    }
}