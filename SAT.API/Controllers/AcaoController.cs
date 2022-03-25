using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            return _acaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codAcao}")]
        public Acao Get(int codAcao)
        {
            return _acaoService.ObterPorCodigo(codAcao);
        }

        [HttpGet]
        [Route("ObterListaAcaoComponente")]
        public ListViewModel ObterListaAcaoComponente([FromQuery] AcaoParameters parameters)
        {
            return _acaoService.ObterListaAcaoComponente(parameters);
        }

        [HttpGet("GetAcaoComponente/{codAcaoComponente}")]
        public AcaoComponente GetAcaoComponente(int codAcaoComponente)
        {
            return _acaoService.ObterAcaoComponentePorCodigo(codAcaoComponente);
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
