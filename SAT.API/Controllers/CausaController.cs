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
    public class CausaController : ControllerBase
    {
        private readonly ICausaRepository _causaInterface;

        public CausaController(ICausaRepository causaInterface)
        {
            _causaInterface = causaInterface;
        }

        [HttpGet]
        public CausaListViewModel Get([FromQuery] CausaParameters parameters)
        {
            var causas = _causaInterface.ObterPorParametros(parameters);

            var lista = new CausaListViewModel
            {
                Causas = causas,
                TotalCount = causas.TotalCount,
                CurrentPage = causas.CurrentPage,
                PageSize = causas.PageSize,
                TotalPages = causas.TotalPages,
                HasNext = causas.HasNext,
                HasPrevious = causas.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codCausa}")]
        public Causa Get(int codCausa)
        {
            return _causaInterface.ObterPorCodigo(codCausa);
        }

        [HttpPost]
        public void Post([FromBody] Causa causa)
        {
            _causaInterface.Criar(causa);
        }

        [HttpPut("{codCausa}")]
        public void Put([FromBody] Causa causa)
        {
            _causaInterface.Atualizar(causa);
        }

        [HttpDelete("{codCausa}")]
        public void Delete(int codCausa)
        {
            _causaInterface.Deletar(codCausa);
        }
    }
}
