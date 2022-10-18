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
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class BancadaLaboratorioController : ControllerBase
    {
        private readonly IBancadaLaboratorioService _bancadaLaboratorioRepo;

        public BancadaLaboratorioController(IBancadaLaboratorioService bancadaLaboratorioService)
        {
            _bancadaLaboratorioRepo = bancadaLaboratorioService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] BancadaLaboratorioParameters parameters)
        {
            return _bancadaLaboratorioRepo.ObterPorParametros(parameters);
        }

        [HttpGet("{numBancada}")]
        public BancadaLaboratorio Get(int numBancada)
        {
            return _bancadaLaboratorioRepo.ObterPorCodigo(numBancada);
        }

        [HttpPost]
        public BancadaLaboratorio Post([FromBody] BancadaLaboratorio lab)
        {
            return _bancadaLaboratorioRepo.Criar(lab);
        }

        [HttpPut]
        public void Put([FromBody] BancadaLaboratorio lab)
        {
            _bancadaLaboratorioRepo.Atualizar(lab);
        }

        [HttpDelete("{numBancada}")]
        public void Delete(int numBancada)
        {
            _bancadaLaboratorioRepo.Deletar(numBancada);
        }
    }
}
