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
    public class IntegracaoClienteController : ControllerBase
    {
        private readonly IIntegracaoClienteService _IntegracaoClienteService;

        public IntegracaoClienteController(
            IIntegracaoClienteService IntegracaoClienteService
        )
        {
            _IntegracaoClienteService = IntegracaoClienteService;
        }

        [HttpPost]
        public IntegracaoCliente Post([FromBody] IntegracaoCliente IntegracaoCliente)
        {
            return _IntegracaoClienteService.Criar(IntegracaoCliente);
        }

        [HttpPut]
        public void Put([FromBody] IntegracaoCliente IntegracaoCliente)
        {
            _IntegracaoClienteService.Atualizar(IntegracaoCliente);
        }

        [HttpDelete("{CodInstalTipoPleito}")]
        public void Delete(int codInstalTipoPleito)
        {
            _IntegracaoClienteService.Deletar(codInstalTipoPleito);
        }
    }
}
