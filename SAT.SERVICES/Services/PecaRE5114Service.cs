using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PecaRE5114Service : IPecaRE5114Service
    {
        private readonly IPecaRE5114Repository _pecaRE5114Repo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public PecaRE5114Service(IPecaRE5114Repository pecaRE5114Repo, ISequenciaRepository sequenciaRepo)
        {
            _pecaRE5114Repo = pecaRE5114Repo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(PecaRE5114Parameters parameters)
        {
            var pecaRE5114s = _pecaRE5114Repo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = pecaRE5114s,
                TotalCount = pecaRE5114s.TotalCount,
                CurrentPage = pecaRE5114s.CurrentPage,
                PageSize = pecaRE5114s.PageSize,
                TotalPages = pecaRE5114s.TotalPages,
                HasNext = pecaRE5114s.HasNext,
                HasPrevious = pecaRE5114s.HasPrevious
            };

            return lista;
        }

        public PecaRE5114 Criar(PecaRE5114 pecaRE5114)
        {
            pecaRE5114.CodPecaRe5114 = _sequenciaRepo.ObterContador("PecaRE5114");
            _pecaRE5114Repo.Criar(pecaRE5114);
            return pecaRE5114;
        }

        public void Deletar(int codigo) =>
            _pecaRE5114Repo.Deletar(codigo);

        public void Atualizar(PecaRE5114 pecaRE5114) =>
            _pecaRE5114Repo.Atualizar(pecaRE5114);

        public PecaRE5114 ObterPorCodigo(int codigo) =>
            _pecaRE5114Repo.ObterPorCodigo(codigo);

    }
}