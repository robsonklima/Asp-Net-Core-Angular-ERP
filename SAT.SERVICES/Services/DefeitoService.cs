using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DefeitoService : IDefeitoService
    {
        private readonly IDefeitoRepository _defeitoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public DefeitoService(
            IDefeitoRepository defeitoRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _defeitoRepo = defeitoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(DefeitoParameters parameters)
        {
            var defeitos = _defeitoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = defeitos,
                TotalCount = defeitos.TotalCount,
                CurrentPage = defeitos.CurrentPage,
                PageSize = defeitos.PageSize,
                TotalPages = defeitos.TotalPages,
                HasNext = defeitos.HasNext,
                HasPrevious = defeitos.HasPrevious
            };

            return lista;
        }

        public Defeito Criar(Defeito defeito)
        {
            defeito.CodDefeito = _sequenciaRepo.ObterContador("Defeito");

            _defeitoRepo.Criar(defeito);
            
            return defeito;
        }

        public void Deletar(int codigo)
        {
            _defeitoRepo.Deletar(codigo);
        }

        public void Atualizar(Defeito defeito)
        {
            _defeitoRepo.Atualizar(defeito);
        }

        public Defeito ObterPorCodigo(int codigo)
        {
            return _defeitoRepo.ObterPorCodigo(codigo);
        }
    }
}
