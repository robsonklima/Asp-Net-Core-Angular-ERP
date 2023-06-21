using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PerfilSetorService : IPerfilSetorService
    {
        private readonly IPerfilSetorRepository _perfilSetorRepo;

        public PerfilSetorService(IPerfilSetorRepository perfilSetorRepo, ISequenciaRepository seqRepo)
        {
            _perfilSetorRepo = perfilSetorRepo;
        }

        public ListViewModel ObterPorParametros(PerfilSetorParameters parameters)
        {
            var perfis = _perfilSetorRepo.ObterPorParametros(parameters);

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

        public PerfilSetor Criar(PerfilSetor perfilSetor)
        {
            _perfilSetorRepo.Criar(perfilSetor);
            return perfilSetor;
        }

        public void Deletar(int codigo)
        {
            _perfilSetorRepo.Deletar(codigo);
        }

        public void Atualizar(PerfilSetor perfilSetor)
        {
            _perfilSetorRepo.Atualizar(perfilSetor);
        }

        public PerfilSetor ObterPorCodigo(int codigo)
        {
            return _perfilSetorRepo.ObterPorCodigo(codigo);
        }
    }
}
