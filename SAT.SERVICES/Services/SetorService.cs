using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _setorRepo;

        public SetorService(ISetorRepository setorRepo, ISequenciaRepository seqRepo)
        {
            _setorRepo = setorRepo;
        }

        public ListViewModel ObterPorParametros(SetorParameters parameters)
        {
            var perfis = _setorRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = perfis,
                TotalCount = perfis.TotalCount,
                CurrentPage = perfis.CurrentPage,
                PageSize = perfis.PageSize,
                TotalPages = perfis.TotalPages,
                HasNext = perfis.HasNext,
                HasPrevious = perfis.HasPrevious
            };

            return lista;
        }

        public Setor Criar(Setor setor)
        {
            _setorRepo.Criar(setor);
            return setor;
        }

        public void Deletar(int codigo)
        {
            _setorRepo.Deletar(codigo);
        }

        public void Atualizar(Setor setor)
        {
            _setorRepo.Atualizar(setor);
        }

        public Setor ObterPorCodigo(int codigo)
        {
            return _setorRepo.ObterPorCodigo(codigo);
        }
    }
}
