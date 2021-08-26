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
    public class PecaController : ControllerBase
    {
        private readonly IPecaService _pecaService;

        public PecaController(IPecaService pecaService)
        {
            _pecaService = pecaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] PecaParameters parameters)
        {
            return _pecaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPeca}")]
        public Peca Get(int codPeca)
        {
            return _pecaService.ObterPorCodigo(codPeca);
        }

        [HttpPost]
        public void Post([FromBody] Peca peca)
        {
            _pecaService.Criar(peca);
        }

        [HttpPut]
        public void Put([FromBody] Peca peca)
        {
            _pecaService.Atualizar(peca);
        }

        [HttpDelete("{codPeca}")]
        public void Delete(int codPeca)
        {
            _pecaService.Deletar(codPeca);
        }
    }
}
