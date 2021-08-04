using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [ApiController]
    [Route("api/[controller]")]
    public class StatusServicoController : ControllerBase
    {
        private readonly IStatusServicoRepository _statusServicoInterface;
        private readonly ISequenciaRepository _sequenciaInterface;
        private readonly ILoggerRepository _logger;

        public StatusServicoController(
            IStatusServicoRepository statusServicoInterface, 
            ISequenciaRepository sequenciaInterface,
            ILoggerRepository logger
        )
        {
            _statusServicoInterface = statusServicoInterface;
            _sequenciaInterface = sequenciaInterface;
            _logger = logger;
        }

        [HttpGet]
        public StatusServicoListViewModel Get([FromQuery] StatusServicoParameters parameters)
        {
            var statusServico = _statusServicoInterface.ObterPorParametros(parameters);

            var statusServicoListaViewModel = new StatusServicoListViewModel
            {
                StatusServico = statusServico,
                TotalCount = statusServico.TotalCount,
                CurrentPage = statusServico.CurrentPage,
                PageSize = statusServico.PageSize,
                TotalPages = statusServico.TotalPages,
                HasNext = statusServico.HasNext,
                HasPrevious = statusServico.HasPrevious
            };

            return statusServicoListaViewModel;
        }

        [HttpGet("{codStatusServico}")]
        public StatusServico Get(int codStatusServico)
        {
            return _statusServicoInterface.ObterPorCodigo(codStatusServico);
        }

        [HttpPost]
        public void Post([FromBody] StatusServico statusServico)
        {
            int codStatusServico = _sequenciaInterface.ObterContador(Constants.TABELA_STATUS_SERVICO);
            statusServico.CodStatusServico = codStatusServico;
            _statusServicoInterface.Criar(statusServico: statusServico);
        }

        [HttpPut]
        public void Put([FromBody] StatusServico statusServico)
        {
            _statusServicoInterface.Atualizar(statusServico);
        }

        [HttpDelete("{codStatusServico:int}")]
        public void Delete(int codStatusServico)
        {
            _statusServicoInterface.Deletar(codStatusServico);
        }
    }
}
