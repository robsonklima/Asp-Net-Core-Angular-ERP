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
    public class RegiaoAutorizadaController : ControllerBase
    {
        private IRegiaoAutorizadaService _regiaoAutorizadaService;

        public RegiaoAutorizadaController(IRegiaoAutorizadaService regiaoAutorizadaService)
        {
            _regiaoAutorizadaService = regiaoAutorizadaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] RegiaoAutorizadaParameters parameters)
        {
            return _regiaoAutorizadaService.ObterPorParametros(parameters);
        }

        [HttpPost]
        public void Post([FromBody] RegiaoAutorizada regiaoAutorizada)
        {
            _regiaoAutorizadaService.Criar(regiaoAutorizada);
        }

        [HttpPut]
        public void Put([FromBody] RegiaoAutorizada regiaoAutorizada)
        {
            _regiaoAutorizadaService.Atualizar(regiaoAutorizada);
        }
    }
}
