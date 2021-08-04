using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NavegacaoController : ControllerBase
    {
        private readonly INavegacaoRepository _navegacaoInterface;

        public NavegacaoController(
            INavegacaoRepository navegacaoInterface
        )
        {
            _navegacaoInterface = navegacaoInterface;
        }

        [HttpGet]
        public NavegacaoListViewModel Get([FromQuery] NavegacaoParameters parameters)
        {
            var navegacoes = _navegacaoInterface.ObterPorParametros(parameters);

            var navegacaoListViewModel = new NavegacaoListViewModel
            {
                Navegacoes = navegacoes,
                TotalCount = navegacoes.TotalCount,
                CurrentPage = navegacoes.CurrentPage,
                PageSize = navegacoes.PageSize,
                TotalPages = navegacoes.TotalPages,
                HasNext = navegacoes.HasNext,
                HasPrevious = navegacoes.HasPrevious
            };

            return navegacaoListViewModel;
        }

        [HttpGet("{codNavegacao}")]
        public Navegacao Get(int codNavegacao)
        {
            return _navegacaoInterface.ObterPorCodigo(codNavegacao);
        }

        [HttpPost]
        public void Post([FromBody] Navegacao navegacao)
        {
            _navegacaoInterface.Criar(navegacao: navegacao);
        }

        [HttpPut]
        public void Put([FromBody] Navegacao navegacao)
        {
            _navegacaoInterface.Atualizar(navegacao);
        }

        [HttpDelete("{codNavegacao}")]
        public void Delete(int codNavegacao)
        {
            _navegacaoInterface.Deletar(codNavegacao);
        }
    }
}
