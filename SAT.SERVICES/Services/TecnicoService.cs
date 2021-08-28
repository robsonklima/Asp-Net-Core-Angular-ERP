using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TecnicoService : ITecnicoService
    {
        private readonly ITecnicoRepository _tecnicosRepo;
        private readonly ISequenciaRepository _seqRepo;

        public TecnicoService(ITecnicoRepository tecnicosRepo, ISequenciaRepository seqRepo)
        {
            _tecnicosRepo = tecnicosRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(TecnicoParameters parameters)
        {
            var tecnicos = _tecnicosRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tecnicos,
                TotalCount = tecnicos.TotalCount,
                CurrentPage = tecnicos.CurrentPage,
                PageSize = tecnicos.PageSize,
                TotalPages = tecnicos.TotalPages,
                HasNext = tecnicos.HasNext,
                HasPrevious = tecnicos.HasPrevious
            };

            return lista;
        }

        public Tecnico Criar(Tecnico tecnico)
        {
            tecnico.CodTecnico = _seqRepo.ObterContador("Tecnico");
            _tecnicosRepo.Criar(tecnico);
            return tecnico;
        }

        public void Deletar(int codigo)
        {
            _tecnicosRepo.Deletar(codigo);
        }

        public void Atualizar(Tecnico tecnico)
        {
            _tecnicosRepo.Atualizar(tecnico);
        }

        public Tecnico ObterPorCodigo(int codigo)
        {
            return _tecnicosRepo.ObterPorCodigo(codigo);
        }
    }
}
