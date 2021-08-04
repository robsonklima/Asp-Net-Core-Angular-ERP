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
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServicoController : ControllerBase
    {
        private readonly ITipoServicoRepository _tipoServicoInterface;
        public ISequenciaRepository _sequenciaInterface { get; }

        public TipoServicoController(
            ITipoServicoRepository tipoServicoInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _tipoServicoInterface = tipoServicoInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet]
        public TipoServicoListViewModel Get([FromQuery] TipoServicoParameters parameters)
        {
            var tiposServico = _tipoServicoInterface.ObterPorParametros(parameters);

            var tipoServicoListaViewModel = new TipoServicoListViewModel
            {
                TiposServico = tiposServico,
                TotalCount = tiposServico.TotalCount,
                CurrentPage = tiposServico.CurrentPage,
                PageSize = tiposServico.PageSize,
                TotalPages = tiposServico.TotalPages,
                HasNext = tiposServico.HasNext,
                HasPrevious = tiposServico.HasPrevious
            };

            return tipoServicoListaViewModel;
        }

        [HttpGet("{codTipoServico}")]
        public TipoServico Get(int codTipoServico)
        {
            return _tipoServicoInterface.ObterPorCodigo(codTipoServico);
        }

        [HttpPost]
        public void Post([FromBody] TipoServico tipoServico)
        {
            int codServico = _sequenciaInterface.ObterContador(Constants.TABELA_TIPO_SERVICO);
            tipoServico.CodServico = codServico;
            _tipoServicoInterface.Criar(tipoServico);
        }

        [HttpPut]
        public void Put([FromBody] TipoServico tipoServico)
        {
            _tipoServicoInterface.Atualizar(tipoServico);
        }

        [HttpDelete("{codServico}")]
        public void Delete(int codServico)
        {
            _tipoServicoInterface.Deletar(codServico);
        }
    }
}
