using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoPeriodoUsuarioStatusService : IPontoPeriodoUsuarioStatusService
    {
        private readonly IPontoPeriodoUsuarioStatusRepository _pontoPeriodoUsuarioStatusRepo;

        public PontoPeriodoUsuarioStatusService(IPontoPeriodoUsuarioStatusRepository pontoPeriodoUsuarioStatusRepo)
        {
            _pontoPeriodoUsuarioStatusRepo = pontoPeriodoUsuarioStatusRepo;
        }

        public ListViewModel ObterPorParametros(PontoPeriodoUsuarioStatusParameters parameters)
        {
            var data = _pontoPeriodoUsuarioStatusRepo.ObterPorParametros(parameters);

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

        public PontoPeriodoUsuarioStatus Criar(PontoPeriodoUsuarioStatus pontoPeriodoUsuario)
        {
            _pontoPeriodoUsuarioStatusRepo.Criar(pontoPeriodoUsuario);
            return pontoPeriodoUsuario;
        }

        public void Deletar(int codigo)
        {
            _pontoPeriodoUsuarioStatusRepo.Deletar(codigo);
        }

        public void Atualizar(PontoPeriodoUsuarioStatus pontoPeriodoUsuario)
        {
            _pontoPeriodoUsuarioStatusRepo.Atualizar(pontoPeriodoUsuario);
        }

        public PontoPeriodoUsuarioStatus ObterPorCodigo(int codigo)
        {
            return _pontoPeriodoUsuarioStatusRepo.ObterPorCodigo(codigo);
        }
    }
}
