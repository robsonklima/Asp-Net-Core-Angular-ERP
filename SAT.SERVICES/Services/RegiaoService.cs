using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _motivoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public RegiaoService(IRegiaoRepository motivoRepo, ISequenciaRepository seqRepo)
        {
            _motivoRepo = motivoRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(RegiaoParameters parameters)
        {
            var regioes = _motivoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = regioes,
                TotalCount = regioes.TotalCount,
                CurrentPage = regioes.CurrentPage,
                PageSize = regioes.PageSize,
                TotalPages = regioes.TotalPages,
                HasNext = regioes.HasNext,
                HasPrevious = regioes.HasPrevious
            };

            return lista;
        }

        public Regiao Criar(Regiao regiao)
        {
            regiao.CodRegiao = _seqRepo.ObterContador("Regiao");
            _motivoRepo.Criar(regiao);
            return regiao;
        }

        public void Deletar(int codigo)
        {
            _motivoRepo.Deletar(codigo);
        }

        public void Atualizar(Regiao regiao)
        {
            _motivoRepo.Atualizar(regiao);
        }

        public Regiao ObterPorCodigo(int codigo)
        {
            return _motivoRepo.ObterPorCodigo(codigo);
        }
    }
}
