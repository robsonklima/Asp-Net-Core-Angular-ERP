using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PontoPeriodoIntervaloAcessoDataService : IPontoPeriodoIntervaloAcessoDataService
    {
        private readonly IPontoPeriodoIntervaloAcessoDataRepository _pontoPeriodoModoAprovacaoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public PontoPeriodoIntervaloAcessoDataService(IPontoPeriodoIntervaloAcessoDataRepository pontoPeriodoModoAprovacaoRepo, ISequenciaRepository seqRepo)
        {
            _pontoPeriodoModoAprovacaoRepo = pontoPeriodoModoAprovacaoRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(PontoPeriodoIntervaloAcessoDataParameters parameters)
        {
            var data = _pontoPeriodoModoAprovacaoRepo.ObterPorParametros(parameters);

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

        public PontoPeriodoIntervaloAcessoData Criar(PontoPeriodoIntervaloAcessoData pontoPeriodoIntervaloAcessoData)
        {
            pontoPeriodoIntervaloAcessoData.CodPontoPeriodoIntervaloAcessoData = _seqRepo.ObterContador("PontoPeriodoIntervaloAcessoData");
            _pontoPeriodoModoAprovacaoRepo.Criar(pontoPeriodoIntervaloAcessoData);
            return pontoPeriodoIntervaloAcessoData;
        }

        public void Deletar(int codigo)
        {
            _pontoPeriodoModoAprovacaoRepo.Deletar(codigo);
        }

        public void Atualizar(PontoPeriodoIntervaloAcessoData pontoPeriodoIntervaloAcessoData)
        {
            _pontoPeriodoModoAprovacaoRepo.Atualizar(pontoPeriodoIntervaloAcessoData);
        }

        public PontoPeriodoIntervaloAcessoData ObterPorCodigo(int codigo)
        {
            return _pontoPeriodoModoAprovacaoRepo.ObterPorCodigo(codigo);
        }
    }
}
