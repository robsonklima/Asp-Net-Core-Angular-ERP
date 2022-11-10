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
    public class ItemDefeitoController : ControllerBase
    {
        private readonly IItemDefeitoService _ItemDefeitoService;

        public ItemDefeitoController(IItemDefeitoService itemDefeitoService)
        {
            _ItemDefeitoService = itemDefeitoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ItemDefeitoParameters parameters)
        {
            return _ItemDefeitoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codItemDefeito}")]
        public ItemDefeito Get(int codItemDefeito)
        {
            return _ItemDefeitoService.ObterPorCodigo(codItemDefeito);
        }

        [HttpPost]
        public void Post([FromBody] ItemDefeito itemDefeito)
        {
            _ItemDefeitoService.Criar(itemDefeito);
        }

        [HttpPut]
        public void Put([FromBody] ItemDefeito itemDefeito)
        {
            _ItemDefeitoService.Atualizar(itemDefeito);
        }

        [HttpDelete("{codItemDefeito}")]
        public void Delete(int codItemDefeito)
        {
            _ItemDefeitoService.Deletar(codItemDefeito);
        }
    }
}
