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
    public class DefeitoController : ControllerBase
    {
        private readonly IDefeitoRepository _defeitoInterface;
        public ISequenciaRepository _sequenciaInterface { get; }

        public DefeitoController(
            IDefeitoRepository defeitoInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _defeitoInterface = defeitoInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet("{codDefeito}")]
        public Defeito Get(int codDefeito)
        {
            return _defeitoInterface.ObterPorCodigo(codDefeito);
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] DefeitoParameters parameters)
        {
            var defeitos = _defeitoInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = defeitos,
                TotalCount = defeitos.TotalCount,
                CurrentPage = defeitos.CurrentPage,
                PageSize = defeitos.PageSize,
                TotalPages = defeitos.TotalPages,
                HasNext = defeitos.HasNext,
                HasPrevious = defeitos.HasPrevious
            };

            return lista;
        }

        [HttpPost]
        public void Post([FromBody] Defeito defeito)
        {
            defeito.CodDefeito = _sequenciaInterface.ObterContador(Constants.TABELA_DEFEITO);

            _defeitoInterface.Criar(defeito);
        }

        [HttpPut]
        public void Put([FromBody] Defeito defeito)
        {
            _defeitoInterface.Atualizar(defeito);
        }

        [HttpDelete("{codDefeito}")]
        public void Delete(int codDefeito)
        {
            _defeitoInterface.Deletar(codDefeito);
        }
    }
}
