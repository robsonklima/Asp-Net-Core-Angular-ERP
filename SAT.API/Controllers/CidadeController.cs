using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;


namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeRepository _cidadeInterface;

        public CidadeController(ICidadeRepository cidadeInterface)
        {
            _cidadeInterface = cidadeInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] CidadeParameters parameters)
        {
            var cidades = _cidadeInterface.ObterPorParametros(parameters);

            var lista= new ListViewModel
            {
                Items = cidades,
                TotalCount = cidades.TotalCount,
                CurrentPage = cidades.CurrentPage,
                PageSize = cidades.PageSize,
                TotalPages = cidades.TotalPages,
                HasNext = cidades.HasNext,
                HasPrevious = cidades.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codCidade}")]
        public Cidade Get(int codCidade)
        {
            return _cidadeInterface.ObterPorCodigo(codCidade);
        }

        [HttpPost]
        public void Post([FromBody] Cidade cidade)
        {
            _cidadeInterface.Criar(cidade);
        }

        [HttpPut("{codCidade}")]
        public void Put([FromBody] Cidade cidade)
        {
            _cidadeInterface.Atualizar(cidade);
        }

        [HttpDelete("{codCidade}")]
        public void Delete(int codCidade)
        {
            _cidadeInterface.Deletar(codCidade);
        }
    }
}
