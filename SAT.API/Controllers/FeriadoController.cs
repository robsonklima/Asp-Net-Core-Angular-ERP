using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;


namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class FeriadoController : ControllerBase
    {
        private readonly IFeriadoRepository _feriadoInterface;

        public FeriadoController(IFeriadoRepository feriadoInterface)
        {
            _feriadoInterface = feriadoInterface;
        }

        [HttpGet]
        public FeriadoListViewModel Get([FromQuery] FeriadoParameters parameters)
        {
            var feriados = _feriadoInterface.ObterPorParametros(parameters);

            var lista = new FeriadoListViewModel
            {
                Feriados = feriados,
                TotalCount = feriados.TotalCount,
                CurrentPage = feriados.CurrentPage,
                PageSize = feriados.PageSize,
                TotalPages = feriados.TotalPages,
                HasNext = feriados.HasNext,
                HasPrevious = feriados.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codFeriado}")]
        public Feriado Get(int codFeriado)
        {
            return _feriadoInterface.ObterPorCodigo(codFeriado);
        }

        [HttpPost]
        public void Post([FromBody] Feriado feriado)
        {
            _feriadoInterface.Criar(feriado);
        }

        [HttpPut]
        public void Put([FromBody] Feriado feriado)
        {
            _feriadoInterface.Atualizar(feriado);
        }

        [HttpDelete("{codFeriado}")]
        public void Delete(int codFeriado)
        {
            _feriadoInterface.Deletar(codFeriado);
        }
    }
}
