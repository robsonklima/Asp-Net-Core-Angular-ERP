using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class PontoUsuarioDataController : ControllerBase
    {
        private readonly IPontoUsuarioDataService _pontoUsuarioDataService;

        public PontoUsuarioDataController(IPontoUsuarioDataService pontoUsuarioDataService)
        {
            _pontoUsuarioDataService = pontoUsuarioDataService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] PontoUsuarioDataParameters parameters)
        {
            return _pontoUsuarioDataService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoUsuarioData}")]
        public PontoUsuarioData Get(int codPontoUsuarioData)
        {
            return _pontoUsuarioDataService.ObterPorCodigo(codPontoUsuarioData);
        }

        [HttpPost]
        public void Post([FromBody] PontoUsuarioData pontoUsuarioData)
        {
            _pontoUsuarioDataService.Criar(pontoUsuarioData: pontoUsuarioData);
        }

        [HttpPut]
        public void Put([FromBody] PontoUsuarioData pontoUsuarioData)
        {
            _pontoUsuarioDataService.Atualizar(pontoUsuarioData: pontoUsuarioData);
        }

        [HttpDelete("{codPontoUsuarioData}")]
        public void Delete(int codPontoUsuarioData)
        {
            _pontoUsuarioDataService.Deletar(codPontoUsuarioData);
        }
    }
}
