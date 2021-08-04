using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;


namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCausaController : ControllerBase
    {
        private readonly ITipoCausaRepository _tipoCausaInterface;

        public TipoCausaController(ITipoCausaRepository tipoCausaInterface)
        {
            _tipoCausaInterface = tipoCausaInterface;
        }

        [HttpGet]
        public TipoCausaListViewModel Get([FromQuery] TipoCausaParameters parameters)
        {
            var tiposCausa = _tipoCausaInterface.ObterPorParametros(parameters);

            var lista = new TipoCausaListViewModel
            {
                TiposCausa = tiposCausa,
                TotalCount = tiposCausa.TotalCount,
                CurrentPage = tiposCausa.CurrentPage,
                PageSize = tiposCausa.PageSize,
                TotalPages = tiposCausa.TotalPages,
                HasNext = tiposCausa.HasNext,
                HasPrevious = tiposCausa.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codTipoCausa}")]
        public TipoCausa Get(int codTipoCausa)
        {
            return _tipoCausaInterface.ObterPorCodigo(codTipoCausa);
        }

        [HttpPost]
        public void Post([FromBody] TipoCausa tipoCausa)
        {
            _tipoCausaInterface.Criar(tipoCausa);
        }

        [HttpPut]
        public void Put([FromBody] TipoCausa tipoCausa)
        {
            _tipoCausaInterface.Atualizar(tipoCausa);
        }

        [HttpDelete("{codTipoCausa}")]
        public void Delete(int codTipoCausa)
        {
            _tipoCausaInterface.Deletar(codTipoCausa);
        }
    }
}
