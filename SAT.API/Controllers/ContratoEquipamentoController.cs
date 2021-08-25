using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoEquipamentoController : ControllerBase
    {
        public IContratoEquipamentoRepository _contratoEquipamentoInterface { get; }

        public ContratoEquipamentoController(
            IContratoEquipamentoRepository contratoEquipamentoInterface
        )
        {
            _contratoEquipamentoInterface = contratoEquipamentoInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ContratoEquipamentoParameters parameters)
        {
            var contratosEquipamento = _contratoEquipamentoInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = contratosEquipamento,
                TotalCount = contratosEquipamento.TotalCount,
                CurrentPage = contratosEquipamento.CurrentPage,
                PageSize = contratosEquipamento.PageSize,
                TotalPages = contratosEquipamento.TotalPages,
                HasNext = contratosEquipamento.HasNext,
                HasPrevious = contratosEquipamento.HasPrevious
            };

            return lista;
        }
    }
}
