using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using Microsoft.AspNetCore.Authorization;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class LocalAtendimentoController : ControllerBase
    {
        private readonly ILocalAtendimentoRepository _localAtendimentoInterface;
        private readonly ISequenciaRepository _sequenciaInterface;
        private readonly ILoggerService _logger;

        public LocalAtendimentoController(
            ILocalAtendimentoRepository localAtendimentoInterface,
            ISequenciaRepository sequenciaInterface,
            ILoggerService logger
        )
        {
            _localAtendimentoInterface = localAtendimentoInterface;
            _sequenciaInterface = sequenciaInterface;
            _logger = logger;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] LocalAtendimentoParameters parameters)
        {
            var locaisAtendimento = _localAtendimentoInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = locaisAtendimento,
                TotalCount = locaisAtendimento.TotalCount,
                CurrentPage = locaisAtendimento.CurrentPage,
                PageSize = locaisAtendimento.PageSize,
                TotalPages = locaisAtendimento.TotalPages,
                HasNext = locaisAtendimento.HasNext,
                HasPrevious = locaisAtendimento.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codPosto}")]
        public LocalAtendimento Get(int codPosto)
        {
            return _localAtendimentoInterface.ObterPorCodigo(codPosto);
        }

        [HttpPost]
        public void Post([FromBody] LocalAtendimento localAtendimento)
        {
            localAtendimento.CodPosto = _sequenciaInterface
                .ObterContador(Constants.TABELA_LOCAL_ATENDIMENTO);
            _localAtendimentoInterface.Criar(localAtendimento);
        }

        [HttpPut]
        public void Put([FromBody] LocalAtendimento localAtendimento)
        {
            _localAtendimentoInterface.Atualizar(localAtendimento);
        }

        [HttpDelete("{codPosto}")]
        public void Delete(int codPosto)
        {
            _localAtendimentoInterface.Deletar(codPosto);
        }
    }
}
