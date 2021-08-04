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
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepository _perfilInterface;
        private readonly ISequenciaRepository _sequenciaInterface;

        public PerfilController(
            IPerfilRepository perfilInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _perfilInterface = perfilInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet]
        public PerfilListViewModel Get([FromQuery] PerfilParameters parameters)
        {
            var perfis = _perfilInterface.ObterPorParametros(parameters);

            var perfilListViewModel = new PerfilListViewModel
            {
                Perfis = perfis,
                TotalCount = perfis.TotalCount,
                CurrentPage = perfis.CurrentPage,
                PageSize = perfis.PageSize,
                TotalPages = perfis.TotalPages,
                HasNext = perfis.HasNext,
                HasPrevious = perfis.HasPrevious
            };

            return perfilListViewModel;
        }

        [HttpGet("{codPerfil}")]
        public Perfil Get(int codPerfil)
        {
            return _perfilInterface.ObterPorCodigo(codPerfil);
        }

        [HttpPost]
        public void Post([FromBody] Perfil perfil)
        {
            perfil.CodPerfil = _sequenciaInterface.ObterContador(Constants.TABELA_PERFIL); ;
            _perfilInterface.Criar(perfil: perfil);
        }

        [HttpPut]
        public void Put([FromBody] Perfil perfil)
        {
            _perfilInterface.Atualizar(perfil: perfil);
        }

        [HttpDelete("{codPerfil}")]
        public void Delete(int codPerfil)
        {
            _perfilInterface.Deletar(codPerfil);
        }
    }
}
