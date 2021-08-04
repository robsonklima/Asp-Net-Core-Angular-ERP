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
    public class AcaoController : ControllerBase
    {
        private readonly IAcaoRepository _acaoInterface;

        public AcaoController(IAcaoRepository acaoInterface)
        {
            _acaoInterface = acaoInterface;
        }

        [HttpGet]
        public AcaoListViewModel Get([FromQuery] AcaoParameters parameters)
        {
            var acoes = _acaoInterface.ObterPorParametros(parameters);

            var acaoListaViewModel = new AcaoListViewModel
            {
                Acoes = acoes,
                TotalCount = acoes.TotalCount,
                CurrentPage = acoes.CurrentPage,
                PageSize = acoes.PageSize,
                TotalPages = acoes.TotalPages,
                HasNext = acoes.HasNext,
                HasPrevious = acoes.HasPrevious
            };

            return acaoListaViewModel;
        }

        [HttpGet("{codAcao}")]
        public Acao Get(int codAcao)
        {
            return _acaoInterface.ObterPorCodigo(codAcao);
        }

        [HttpPost]
        public void Post([FromBody] Acao acao)
        {
            _acaoInterface.Criar(acao);
        }

        [HttpPut]
        public void Put([FromBody] Acao acao)
        {

        }

        [HttpDelete("{codAcao}")]
        public void Delete(int codAcao)
        {

        }
    }
}
