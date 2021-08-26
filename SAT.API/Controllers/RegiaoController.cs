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
    public class RegiaoController : ControllerBase
    {
        private IRegiaoService _regiaoService;

        public RegiaoController(
            IRegiaoService regiaoService
        )
        {
            _regiaoService = regiaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] RegiaoParameters parameters)
        {
            return _regiaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codRegiao}")]
        public Regiao Get(int codRegiao)
        {
            return _regiaoService.ObterPorCodigo(codRegiao);
        }

        [HttpPost]
        public void Post([FromBody] Regiao regiao)
        {
            _regiaoService.Criar(regiao: regiao);
        }

        [HttpPut]
        public void Put([FromBody] Regiao regiao)
        {
            _regiaoService.Atualizar(regiao);
        }

        [HttpDelete("{codRegiao}")]
        public void Delete(int codRegiao)
        {
            _regiaoService.Deletar(codRegiao);
        }
    }
}
