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
    public class AcordoNivelServicoController : ControllerBase
    {
        private IAcordoNivelServicoRepository _acordoNivelServicoInterface;
        private readonly ISequenciaRepository _sequenciaInterface;

        public AcordoNivelServicoController(
            IAcordoNivelServicoRepository acordoNivelServicoInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _acordoNivelServicoInterface = acordoNivelServicoInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet]
        public AcordoNivelServicoListViewModel Get([FromQuery] AcordoNivelServicoParameters parameters)
        {
            var ans = _acordoNivelServicoInterface.ObterPorParametros(parameters);

            var ansListViewModel = new AcordoNivelServicoListViewModel
            {
                AcordosNivelServico = ans,
                TotalCount = ans.TotalCount,
                CurrentPage = ans.CurrentPage,
                PageSize = ans.PageSize,
                TotalPages = ans.TotalPages,
                HasNext = ans.HasNext,
                HasPrevious = ans.HasPrevious
            };

            return ansListViewModel;
        }

        [HttpPost]
        public void Post([FromBody] AcordoNivelServico acordoNivelServico)
        {
            acordoNivelServico.CodSLA = _sequenciaInterface.ObterContador(Constants.TABELA_ACORDO_NIVEL_SERVICO); ;
            _acordoNivelServicoInterface.Criar(acordoNivelServico: acordoNivelServico);
        }

        [HttpGet("{codSLA}")]
        public AcordoNivelServico Get(int codSLA)
        {
            return _acordoNivelServicoInterface.ObterPorCodigo(codSLA);
        }

        [HttpPut("{codSLA}")]
        public void Put([FromBody] AcordoNivelServico acordoNivelServico)
        {
            _acordoNivelServicoInterface.Atualizar(acordoNivelServico: acordoNivelServico);
        }

        [HttpDelete("{codSLA}")]
        public void Delete(int codSLA)
        {
            _acordoNivelServicoInterface.Deletar(codigo: codSLA);
        }
    }
}
