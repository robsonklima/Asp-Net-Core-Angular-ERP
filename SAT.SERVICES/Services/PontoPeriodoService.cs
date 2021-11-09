using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoPeriodoService : IPontoPeriodoService
    {
        private readonly IPontoPeriodoRepository _pontoPeriodoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public PontoPeriodoService(IPontoPeriodoRepository pontoPeriodoRepo, ISequenciaRepository seqRepo)
        {
            _pontoPeriodoRepo = pontoPeriodoRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(PontoPeriodoParameters parameters)
        {
            var data = _pontoPeriodoRepo.ObterPorParametros(parameters);

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

        public PontoPeriodo Criar(PontoPeriodo pontoUsuario)
        {
            pontoUsuario.CodPontoPeriodo = _seqRepo.ObterContador("PontoPeriodo");
            _pontoPeriodoRepo.Criar(pontoUsuario);
            return pontoUsuario;
        }

        public void Deletar(int codigo)
        {
            _pontoPeriodoRepo.Deletar(codigo);
        }

        public void Atualizar(PontoPeriodo pontoUsuario)
        {
            _pontoPeriodoRepo.Atualizar(pontoUsuario);
        }

        public PontoPeriodo ObterPorCodigo(int codigo)
        {
            return _pontoPeriodoRepo.ObterPorCodigo(codigo);
        }
    }
}
