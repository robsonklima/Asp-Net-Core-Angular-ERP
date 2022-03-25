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
    public class FerramentaTecnicoController : ControllerBase
    {
        private readonly IFerramentaTecnicoService _FerramentaTecnicosService;

        public FerramentaTecnicoController(IFerramentaTecnicoService FerramentaTecnicosService)
        {
            _FerramentaTecnicosService = FerramentaTecnicosService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] FerramentaTecnicoParameters parameters)
        {
            return _FerramentaTecnicosService.ObterPorParametros(parameters);
        }

        [HttpGet("{codFerramentaTecnico}")]
        public FerramentaTecnico Get(int codFerramentaTecnico)
        {
            return _FerramentaTecnicosService.ObterPorCodigo(codFerramentaTecnico);
        }

        [HttpPost]
        public FerramentaTecnico Post([FromBody] FerramentaTecnico FerramentaTecnicos)
        {
            return _FerramentaTecnicosService.Criar(FerramentaTecnicos);
        }

        [HttpPut]
        public void Put([FromBody] FerramentaTecnico FerramentaTecnicos)
        {
            _FerramentaTecnicosService.Atualizar(FerramentaTecnicos);
        }

        [HttpDelete("{codFerramentaTecnico}")]
        public void Delete(int codFerramentaTecnicos)
        {
            _FerramentaTecnicosService.Deletar(codFerramentaTecnicos);
        }
    }
}
