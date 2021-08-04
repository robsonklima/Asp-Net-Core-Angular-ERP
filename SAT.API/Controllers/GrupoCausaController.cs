using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;


namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoCausaController : ControllerBase
    {
        private readonly IGrupoCausaRepository _grupoCausaInterface;

        public GrupoCausaController(IGrupoCausaRepository grupoCausaInterface)
        {
            _grupoCausaInterface = grupoCausaInterface;
        }

        [HttpGet]
        public GrupoCausaListViewModel Get([FromQuery] GrupoCausaParameters parameters)
        {
            var gruposCausa = _grupoCausaInterface.ObterPorParametros(parameters);

            var lista = new GrupoCausaListViewModel
            {
                GruposCausa = gruposCausa,
                TotalCount = gruposCausa.TotalCount,
                CurrentPage = gruposCausa.CurrentPage,
                PageSize = gruposCausa.PageSize,
                TotalPages = gruposCausa.TotalPages,
                HasNext = gruposCausa.HasNext,
                HasPrevious = gruposCausa.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codGrupoCausa}")]
        public GrupoCausa Get(int codGrupoCausa)
        {
            return _grupoCausaInterface.ObterPorCodigo(codGrupoCausa);
        }

        [HttpPost]
        public void Post([FromBody] GrupoCausa grupoCausa)
        {
            _grupoCausaInterface.Criar(grupoCausa);
        }

        [HttpPut]
        public void Put([FromBody] GrupoCausa grupoCausa)
        {
            _grupoCausaInterface.Atualizar(grupoCausa);
        }

        [HttpDelete("{codGrupoCausa}")]
        public void Delete(int codGrupoCausa)
        {
            _grupoCausaInterface.Deletar(codGrupoCausa);
        }
    }
}
