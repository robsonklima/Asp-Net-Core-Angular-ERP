using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class RelatorioAtendimentoDetalhePecaController : ControllerBase
    {
        private readonly IRelatorioAtendimentoDetalhePecaService _rdpService;

        public RelatorioAtendimentoDetalhePecaController(
            IRelatorioAtendimentoDetalhePecaService rdpService
        )
        {
            _rdpService = rdpService;
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public RelatorioAtendimentoDetalhePeca Post([FromBody] RelatorioAtendimentoDetalhePeca detalhePeca)
        {
            _rdpService.Criar(detalhePeca);

            return detalhePeca;
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] RelatorioAtendimentoDetalhePeca detalhePeca)
        {
            _rdpService.Atualizar(detalhePeca);
        }

        [HttpDelete("{codRATDetalhePeca}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codRATDetalhePeca)
        {
            _rdpService.Deletar(codRATDetalhePeca);
        }
    }
}
