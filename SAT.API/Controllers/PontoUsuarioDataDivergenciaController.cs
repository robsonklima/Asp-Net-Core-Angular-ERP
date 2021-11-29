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
    public class PontoUsuarioDataDivergenciaController : ControllerBase
    {
        private readonly IPontoUsuarioDataDivergenciaService _pontoUsuarioDataDivergenciaService;

        public PontoUsuarioDataDivergenciaController(IPontoUsuarioDataDivergenciaService pontoUsuarioDataDivergenciaService)
        {
            _pontoUsuarioDataDivergenciaService = pontoUsuarioDataDivergenciaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] PontoUsuarioDataDivergenciaParameters parameters)
        {
            return _pontoUsuarioDataDivergenciaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoUsuarioDataDivergencia}")]
        public PontoUsuarioDataDivergencia Get(int codPontoUsuarioDataDivergencia)
        {
            return _pontoUsuarioDataDivergenciaService.ObterPorCodigo(codPontoUsuarioDataDivergencia);
        }

        [HttpPost]
        public void Post([FromBody] PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia)
        {
            _pontoUsuarioDataDivergenciaService.Criar(pontoUsuarioDataDivergencia: pontoUsuarioDataDivergencia);
        }

        [HttpPut]
        public void Put([FromBody] PontoUsuarioDataDivergencia pontoUsuarioDataDivergencia)
        {
            _pontoUsuarioDataDivergenciaService.Atualizar(pontoUsuarioDataDivergencia: pontoUsuarioDataDivergencia);
        }

        [HttpDelete("{codPontoUsuarioDataDivergencia}")]
        public void Delete(int codPontoUsuarioDataDivergencia)
        {
            _pontoUsuarioDataDivergenciaService.Deletar(codPontoUsuarioDataDivergencia);
        }
    }
}
