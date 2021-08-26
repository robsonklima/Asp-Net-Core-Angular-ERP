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
    public class UnidadeFederativaController : ControllerBase
    {
        private readonly IUnidadeFederativaService _unidadeFederativaService;

        public UnidadeFederativaController(IUnidadeFederativaService unidadeFederativaService)
        {
            _unidadeFederativaService = unidadeFederativaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] UnidadeFederativaParameters parameteres)
        {
            return _unidadeFederativaService.ObterPorParametros(parameteres);
        }

        [HttpGet("{codUF}")]
        public UnidadeFederativa Get(int codUF)
        {
            return _unidadeFederativaService.ObterPorCodigo(codUF);
        }

        [HttpPost]
        public void Post([FromBody] UnidadeFederativa uf)
        {
            _unidadeFederativaService.Criar(uf);
        }

        [HttpPut]
        public void Put([FromBody] UnidadeFederativa uf)
        {
            _unidadeFederativaService.Atualizar(uf);
        }

        [HttpDelete("{codUF}")]
        public void Delete(int codUF)
        {
            _unidadeFederativaService.Deletar(codUF);
        }
    }
}
