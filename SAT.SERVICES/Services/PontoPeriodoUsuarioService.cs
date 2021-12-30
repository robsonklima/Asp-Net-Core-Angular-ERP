using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoPeriodoUsuarioService : IPontoPeriodoUsuarioService
    {
        private readonly IPontoPeriodoUsuarioRepository _pontoPeriodoStatusRepo;

        public PontoPeriodoUsuarioService(IPontoPeriodoUsuarioRepository pontoPeriodoStatusRepo)
        {
            _pontoPeriodoStatusRepo = pontoPeriodoStatusRepo;
        }

        public ListViewModel ObterPorParametros(PontoPeriodoUsuarioParameters parameters)
        {
            var data = _pontoPeriodoStatusRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = data,
                TotalCount = data.TotalCount,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                HasNext = data.HasNext,
                HasPrevious = data.HasPrevious
            };

            return lista;
        }

        public PontoPeriodoUsuario Criar(PontoPeriodoUsuario pontoPeriodoUsuario)
        {
            _pontoPeriodoStatusRepo.Criar(pontoPeriodoUsuario);
            return pontoPeriodoUsuario;
        }

        public void Deletar(int codigo)
        {
            _pontoPeriodoStatusRepo.Deletar(codigo);
        }

        public void Atualizar(PontoPeriodoUsuario pontoPeriodoUsuario)
        {
            _pontoPeriodoStatusRepo.Atualizar(pontoPeriodoUsuario);
        }

        public PontoPeriodoUsuario ObterPorCodigo(int codigo)
        {
            return _pontoPeriodoStatusRepo.ObterPorCodigo(codigo);
        }
    }
}
