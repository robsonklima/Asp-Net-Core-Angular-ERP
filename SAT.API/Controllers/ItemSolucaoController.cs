using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemSolucaoController : ControllerBase
    {
        private readonly IItemSolucaoService _ItemSolucaoService;

        public ItemSolucaoController(IItemSolucaoService itemSolucaoService)
        {
            _ItemSolucaoService = itemSolucaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ItemSolucaoParameters parameters)
        {
            return _ItemSolucaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codItemSolucao}")]
        public ItemSolucao Get(int codItemSolucao)
        {
            return _ItemSolucaoService.ObterPorCodigo(codItemSolucao);
        }

        [HttpPost]
        public void Post([FromBody] ItemSolucao itemSolucao)
        {
            _ItemSolucaoService.Criar(itemSolucao);
        }

        [HttpPut]
        public void Put([FromBody] ItemSolucao itemSolucao)
        {
            _ItemSolucaoService.Atualizar(itemSolucao);
        }

        [HttpDelete("{codItemSolucao}")]
        public void Delete(int codItemSolucao)
        {
            _ItemSolucaoService.Deletar(codItemSolucao);
        }
    }
}
