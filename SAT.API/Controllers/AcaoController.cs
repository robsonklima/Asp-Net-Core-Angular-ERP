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
    public class AcaoController : ControllerBase
    {
        private readonly IAcaoService _acaoService;

        public AcaoController(IAcaoService acaoService)
        {
            _acaoService = acaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] AcaoParameters parameters)
        {
            var acoes = _acaoService.ObterPorParametros(parameters);

            var acaoListaViewModel = new ListViewModel
            {
                Items = acoes,
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
            return _acaoService.ObterPorCodigo(codAcao);
        }

        [HttpPost]
        public void Post([FromBody] Acao acao)
        {
            _acaoService.Criar(acao);
        }

        [HttpPut]
        public void Put([FromBody] Acao acao)
        {
            _acaoService.Atualizar(acao);
        }

        [HttpDelete("{codAcao}")]
        public void Delete(int codAcao)
        {
            _acaoService.Deletar(codAcao);
        }
    }
}
