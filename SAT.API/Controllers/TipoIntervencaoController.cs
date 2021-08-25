using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;


namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoIntervencaoController : ControllerBase
    {
        private readonly ITipoIntervencaoRepository _tipoIntervencaoInterface;
        public ISequenciaRepository _sequenciaInterface { get; }

        public TipoIntervencaoController(
            ITipoIntervencaoRepository tipoIntervencaoInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _tipoIntervencaoInterface = tipoIntervencaoInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TipoIntervencaoParameters parameters)
        {
            var tiposIntervencao = _tipoIntervencaoInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tiposIntervencao,
                TotalCount = tiposIntervencao.TotalCount,
                CurrentPage = tiposIntervencao.CurrentPage,
                PageSize = tiposIntervencao.PageSize,
                TotalPages = tiposIntervencao.TotalPages,
                HasNext = tiposIntervencao.HasNext,
                HasPrevious = tiposIntervencao.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codTipoIntervencao}")]
        public TipoIntervencao Get(int codTipoIntervencao)
        {
            return _tipoIntervencaoInterface.ObterPorCodigo(codTipoIntervencao);
        }

        [HttpPost]
        public void Post([FromBody] TipoIntervencao tipoIntervencao)
        {
            int codigo = _sequenciaInterface.ObterContador(Constants.TABELA_TIPO_INTERVENCAO);
            tipoIntervencao.CodTipoIntervencao = codigo;
            _tipoIntervencaoInterface.Criar(tipoIntervencao);
        }

        [HttpPut]
        public void Put([FromBody] TipoIntervencao tipoIntervencao)
        {
            _tipoIntervencaoInterface.Atualizar(tipoIntervencao);
        }

        [HttpDelete("{codTipoIntervencao}")]
        public void Delete(int codTipoIntervencao)
        {
            _tipoIntervencaoInterface.Deletar(codTipoIntervencao);
        }
    }
}
