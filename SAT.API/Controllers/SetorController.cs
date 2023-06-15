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
    public class SetorController : ControllerBase
    {
        private readonly ISetorService _setorService;

        public SetorController(ISetorService setorService)
        {
            _setorService = setorService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] SetorParameters parameters)
        {
            return _setorService.ObterPorParametros(parameters);
        }

        [HttpGet("{codSetor}")]
        public Setor Get(int codSetor)
        {
            return _setorService.ObterPorCodigo(codSetor);
        }

        [HttpPost]
        public void Post([FromBody] Setor setor)
        {
            _setorService.Criar(setor: setor);
        }

        [HttpPut]
        public void Put([FromBody] Setor setor)
        {
            _setorService.Atualizar(setor: setor);
        }

        [HttpDelete("{codSetor}")]
        public void Delete(int codSetor)
        {
            _setorService.Deletar(codSetor);
        }
    }
}
