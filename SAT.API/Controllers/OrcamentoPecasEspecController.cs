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
    public class OrcamentoPecasEspecController : ControllerBase
    {
        private readonly IOrcamentoPecasEspecService _OrcamentoPecasEspecService;

        public OrcamentoPecasEspecController(IOrcamentoPecasEspecService OrcamentoPecasEspecService)
        {
            _OrcamentoPecasEspecService = OrcamentoPecasEspecService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrcamentoPecasEspecParameters parameters)
        {
            return _OrcamentoPecasEspecService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodOrcamentoPecasEspec}")]
        public OrcamentoPecasEspec Get(int CodOrcamentoPecasEspec)
        {
            return _OrcamentoPecasEspecService.ObterPorCodigo(CodOrcamentoPecasEspec);
        }

        [HttpPost]
        public OrcamentoPecasEspec Post([FromBody] OrcamentoPecasEspec OrcamentoPecasEspec)
        {
            return _OrcamentoPecasEspecService.Criar(OrcamentoPecasEspec);
        }

        [HttpPut]
        public void Put([FromBody] OrcamentoPecasEspec OrcamentoPecasEspec)
        {
            _OrcamentoPecasEspecService.Atualizar(OrcamentoPecasEspec);
        }

        [HttpDelete("{CodOrcamentoPecasEspec}")]
        public void Delete(int codOrcamentoPecasEspec)
        {
            _OrcamentoPecasEspecService.Deletar(codOrcamentoPecasEspec);
        }
    }
}
