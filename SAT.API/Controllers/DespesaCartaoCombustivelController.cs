using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaCartaoCombustivelController : ControllerBase
    {
        public IDespesaCartaoCombustivelRepository _despesaCartaoCombustivelInterface { get; }

        public DespesaCartaoCombustivelController(
            IDespesaCartaoCombustivelRepository despesaCartaoCombustivelInterface
        )
        {
            _despesaCartaoCombustivelInterface = despesaCartaoCombustivelInterface;
        }

        [HttpGet]
        public DespesaCartaoCombustivelListViewModel Get([FromQuery] DespesaCartaoCombustivelParameters parameters)
        {
            var cartoes = _despesaCartaoCombustivelInterface.ObterPorParametros(parameters);

            var lista = new DespesaCartaoCombustivelListViewModel
            {
                DespesaCartoesCombustivel = cartoes,
                TotalCount = cartoes.TotalCount,
                CurrentPage = cartoes.CurrentPage,
                PageSize = cartoes.PageSize,
                TotalPages = cartoes.TotalPages,
                HasNext = cartoes.HasNext,
                HasPrevious = cartoes.HasPrevious
            };

            return lista;
        }
    }
}
