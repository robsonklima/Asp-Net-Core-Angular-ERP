using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities;
using System.Collections.Generic;
using SAT.MODELS.Entities.Constants;
using Microsoft.AspNetCore.Authorization;

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
        private readonly ILoggerRepository _logger;

        public LocalAtendimentoController(
            ILocalAtendimentoRepository localAtendimentoInterface,
            ISequenciaRepository sequenciaInterface,
            ILoggerRepository logger
        )
        {
            _localAtendimentoInterface = localAtendimentoInterface;
            _sequenciaInterface = sequenciaInterface;
            _logger = logger;
        }

        [HttpGet]
        public LocalAtendimentoListViewModel Get([FromQuery] LocalAtendimentoParameters parameters)
        {
            var locaisAtendimento = _localAtendimentoInterface.ObterPorParametros(parameters);

            var clienteListaViewModel = new LocalAtendimentoListViewModel
            {
                LocaisAtendimento = locaisAtendimento,
                TotalCount = locaisAtendimento.TotalCount,
                CurrentPage = locaisAtendimento.CurrentPage,
                PageSize = locaisAtendimento.PageSize,
                TotalPages = locaisAtendimento.TotalPages,
                HasNext = locaisAtendimento.HasNext,
                HasPrevious = locaisAtendimento.HasPrevious
            };

            return clienteListaViewModel;
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
