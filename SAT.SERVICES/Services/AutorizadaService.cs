using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AutorizadaService : IAutorizadaService
    {
        private IAutorizadaRepository _autorizadaRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public AutorizadaService(
            IAutorizadaRepository autorizadaRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _autorizadaRepo = autorizadaRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(AutorizadaParameters parameters)
        {
            var autorizadas = _autorizadaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = autorizadas,
                TotalCount = autorizadas.TotalCount,
                CurrentPage = autorizadas.CurrentPage,
                PageSize = autorizadas.PageSize,
                TotalPages = autorizadas.TotalPages,
                HasNext = autorizadas.HasNext,
                HasPrevious = autorizadas.HasPrevious
            };

            return lista;
        }

        public Autorizada Criar(Autorizada autorizada)
        {
            autorizada.CodAutorizada = _sequenciaRepo.ObterContador("Autorizada");
            _autorizadaRepo.Criar(autorizada: autorizada);
            return autorizada;
        }

        public void Deletar(int codigo)
        {
            _autorizadaRepo.Deletar(codigo);
        }

        public void Atualizar(Autorizada autorizada)
        {
            _autorizadaRepo.Atualizar(autorizada);
        }

        public Autorizada ObterPorCodigo(int codigo)
        {
            return _autorizadaRepo.ObterPorCodigo(codigo);
        }
    }
}
