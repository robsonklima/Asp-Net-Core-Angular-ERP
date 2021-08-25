using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class OrdemServicoController : ControllerBase
    {
        private readonly IOrdemServicoRepository _ordemServicoInterface;
        private readonly ISequenciaRepository _sequenciaInterface;

        public OrdemServicoController(
            IOrdemServicoRepository ordemServicoInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _ordemServicoInterface = ordemServicoInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OrdemServicoParameters parameters)
        {
            var ordensServico = _ordemServicoInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ordensServico,
                TotalCount = ordensServico.TotalCount,
                CurrentPage = ordensServico.CurrentPage,
                PageSize = ordensServico.PageSize,
                TotalPages = ordensServico.TotalPages,
                HasNext = ordensServico.HasNext,
                HasPrevious = ordensServico.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codOS}")]
        public OrdemServico Get(int codOS)
        {
            return _ordemServicoInterface.ObterPorCodigo(codOS);
        }

        [HttpPost]
        public OrdemServico Post([FromBody] OrdemServico ordemServico)
        {
            _ordemServicoInterface.Criar(ordemServico);

            return ordemServico;
        }

        [HttpPut]
        public void Put([FromBody] OrdemServico ordemServico)
        {
            _ordemServicoInterface.Atualizar(ordemServico);
        }

        [HttpDelete("{codOS}")]
        public void Delete(int codOS)
        {
            _ordemServicoInterface.Deletar(codOS);
        }
    }
}
