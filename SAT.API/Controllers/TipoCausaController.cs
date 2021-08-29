using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class TipoCausaController : ControllerBase
    {
        private readonly ITipoCausaService _tipoCausaService;

        public TipoCausaController(ITipoCausaService tipoCausaService)
        {
            _tipoCausaService = tipoCausaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TipoCausaParameters parameters)
        {
            return _tipoCausaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTipoCausa}")]
        public TipoCausa Get(int codTipoCausa)
        {
            return _tipoCausaService.ObterPorCodigo(codTipoCausa);
        }

        [HttpPost]
        public void Post([FromBody] TipoCausa tipoCausa)
        {
            _tipoCausaService.Criar(tipoCausa);
        }

        [HttpPut]
        public void Put([FromBody] TipoCausa tipoCausa)
        {
            _tipoCausaService.Atualizar(tipoCausa);
        }

        [HttpDelete("{codTipoCausa}")]
        public void Delete(int codTipoCausa)
        {
            _tipoCausaService.Deletar(codTipoCausa);
        }
    }
}
