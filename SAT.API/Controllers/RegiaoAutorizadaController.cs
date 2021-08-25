using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;


namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegiaoAutorizadaController : ControllerBase
    {
        private IRegiaoAutorizadaRepository _regiaoAutorizadaInterface;

        public RegiaoAutorizadaController(IRegiaoAutorizadaRepository regiaoAutorizadaInterface)
        {
            _regiaoAutorizadaInterface = regiaoAutorizadaInterface;
        }

        [HttpGet("{codRegiao}/{codAutorizada}/codFilial")]
        public RegiaoAutorizada Get(int codRegiao, int codAutorizada, int codFilial)
        {
            return _regiaoAutorizadaInterface.ObterPorCodigo(codRegiao, codAutorizada, codFilial);
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] RegiaoAutorizadaParameters parameters)
        {
            var regioesAutorizadas = _regiaoAutorizadaInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = regioesAutorizadas,
                TotalCount = regioesAutorizadas.TotalCount,
                CurrentPage = regioesAutorizadas.CurrentPage,
                PageSize = regioesAutorizadas.PageSize,
                TotalPages = regioesAutorizadas.TotalPages,
                HasNext = regioesAutorizadas.HasNext,
                HasPrevious = regioesAutorizadas.HasPrevious
            };

            return lista;
        }

        [HttpPost]
        public void Post([FromBody] RegiaoAutorizada regiaoAutorizada)
        {
            _regiaoAutorizadaInterface.Criar(regiaoAutorizada);
        }

        [HttpPut]
        public void Put([FromBody] RegiaoAutorizada regiaoAutorizada)
        {
            _regiaoAutorizadaInterface.Atualizar(regiaoAutorizada);
        }

        [HttpDelete("{codRegiao}/{codAutorizada}/{codFilial}")]
        public void Delete(int codRegiao, int codAutorizada, int codFilial)
        {
            _regiaoAutorizadaInterface.Deletar(codRegiao, codAutorizada, codFilial);
        }
    }
}
