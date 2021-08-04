using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;


namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizadaController : ControllerBase
    {
        private IAutorizadaRepository _autorizadaInterface;
        private readonly ISequenciaRepository _sequenciaInterface;

        public AutorizadaController(
            IAutorizadaRepository autorizadaInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _autorizadaInterface = autorizadaInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet]
        public AutorizadaListViewModel Get([FromQuery] AutorizadaParameters parameters)
        {
            var autorizadas = _autorizadaInterface.ObterPorParametros(parameters);

            var autorizadaListViewModel = new AutorizadaListViewModel
            {
                Autorizadas = autorizadas,
                TotalCount = autorizadas.TotalCount,
                CurrentPage = autorizadas.CurrentPage,
                PageSize = autorizadas.PageSize,
                TotalPages = autorizadas.TotalPages,
                HasNext = autorizadas.HasNext,
                HasPrevious = autorizadas.HasPrevious
            };

            return autorizadaListViewModel;
        }

        [HttpGet("{codAutorizada}")]
        public Autorizada Get(int codAutorizada)
        {
            return _autorizadaInterface.ObterPorCodigo(codAutorizada);
        }

        [HttpPost]
        public void Post([FromBody] Autorizada autorizada)
        {
            autorizada.CodAutorizada = _sequenciaInterface.ObterContador(Constants.TABELA_AUTORIZADA); ;
            _autorizadaInterface.Criar(autorizada: autorizada);
        }

        [HttpPut("{codAutorizada}")]
        public void Put([FromBody] Autorizada autorizada)
        {
            _autorizadaInterface.Atualizar(autorizada: autorizada);
        }

        [HttpDelete("{codAutorizada}")]
        public void Delete(int codAutorizada)
        {
            _autorizadaInterface.Deletar(codAutorizada);
        }
    }
}
