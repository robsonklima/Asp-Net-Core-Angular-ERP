using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoPeriodoStatusService : IPontoPeriodoStatusService
    {
        private readonly IPontoPeriodoStatusRepository _pontoPeriodoStatusRepo;
        private readonly ISequenciaRepository _seqRepo;

        public PontoPeriodoStatusService(IPontoPeriodoStatusRepository pontoPeriodoStatusRepo, ISequenciaRepository seqRepo)
        {
            _pontoPeriodoStatusRepo = pontoPeriodoStatusRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(PontoPeriodoStatusParameters parameters)
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

        public PontoPeriodoStatus Criar(PontoPeriodoStatus pontoUsuario)
        {
            pontoUsuario.CodPontoPeriodoStatus = _seqRepo.ObterContador("PontoPeriodoStatus");
            _pontoPeriodoStatusRepo.Criar(pontoUsuario);
            return pontoUsuario;
        }

        public void Deletar(int codigo)
        {
            _pontoPeriodoStatusRepo.Deletar(codigo);
        }

        public void Atualizar(PontoPeriodoStatus pontoUsuario)
        {
            _pontoPeriodoStatusRepo.Atualizar(pontoUsuario);
        }

        public PontoPeriodoStatus ObterPorCodigo(int codigo)
        {
            return _pontoPeriodoStatusRepo.ObterPorCodigo(codigo);
        }
    }
}
