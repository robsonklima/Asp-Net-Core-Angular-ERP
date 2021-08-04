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
    public class RegiaoController : ControllerBase
    {
        private IRegiaoRepository _regiaoInterface;
        private readonly ISequenciaRepository _sequenciaInterface;

        public RegiaoController(
            IRegiaoRepository regiaoInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _regiaoInterface = regiaoInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet]
        public RegiaoListViewModel Get([FromQuery] RegiaoParameters parameters)
        {
            var regioes = _regiaoInterface.ObterPorParametros(parameters);

            var regiaoListViewModel = new RegiaoListViewModel
            {
                Regioes = regioes,
                TotalCount = regioes.TotalCount,
                CurrentPage = regioes.CurrentPage,
                PageSize = regioes.PageSize,
                TotalPages = regioes.TotalPages,
                HasNext = regioes.HasNext,
                HasPrevious = regioes.HasPrevious
            };

            return regiaoListViewModel;
        }

        [HttpGet("{codRegiao}")]
        public Regiao Get(int codRegiao)
        {
            return _regiaoInterface.ObterPorCodigo(codRegiao);
        }

        [HttpPost]
        public void Post([FromBody] Regiao regiao)
        {
            regiao.CodRegiao = _sequenciaInterface.ObterContador(Constants.TABELA_REGIAO);

            _regiaoInterface.Criar(regiao: regiao);
        }

        [HttpPut]
        public void Put([FromBody] Regiao regiao)
        {
            _regiaoInterface.Atualizar(regiao);
        }

        [HttpDelete("{codRegiao}")]
        public void Delete(int codRegiao)
        {
            _regiaoInterface.Deletar(codRegiao);
        }
    }
}
