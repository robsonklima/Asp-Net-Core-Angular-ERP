using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepo;
        private readonly ISequenciaRepository _seqRepo;

        public PerfilService(IPerfilRepository perfilRepo, ISequenciaRepository seqRepo)
        {
            _perfilRepo = perfilRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(PerfilParameters parameters)
        {
            var perfis = _perfilRepo.ObterPorParametros(parameters);

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

        public Perfil Criar(Perfil perfil)
        {
            perfil.CodPerfil = _seqRepo.ObterContador(Constants.TABELA_PERFIL);
            _perfilRepo.Criar(perfil);
            return perfil;
        }

        public void Deletar(int codigo)
        {
            _perfilRepo.Deletar(codigo);
        }

        public void Atualizar(Perfil perfil)
        {
            _perfilRepo.Atualizar(perfil);
        }

        public Perfil ObterPorCodigo(int codigo)
        {
            return _perfilRepo.ObterPorCodigo(codigo);
        }
    }
}
