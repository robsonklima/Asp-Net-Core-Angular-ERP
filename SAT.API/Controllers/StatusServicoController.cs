using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

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
        private readonly ILoggerService _logger;

        public StatusServicoController(
            IStatusServicoRepository statusServicoInterface, 
            ISequenciaRepository sequenciaInterface,
            ILoggerService logger
        )
        {
            _statusServicoInterface = statusServicoInterface;
            _sequenciaInterface = sequenciaInterface;
            _logger = logger;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] StatusServicoParameters parameters)
        {
            var statusServico = _statusServicoInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = statusServico,
                TotalCount = statusServico.TotalCount,
                CurrentPage = statusServico.CurrentPage,
                PageSize = statusServico.PageSize,
                TotalPages = statusServico.TotalPages,
                HasNext = statusServico.HasNext,
                HasPrevious = statusServico.HasPrevious
            };

            return lista;
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
