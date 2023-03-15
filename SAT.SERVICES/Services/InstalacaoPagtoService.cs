using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoPagtoService : IInstalacaoPagtoService
    {
        private readonly IInstalacaoPagtoRepository _instalacaoPagtoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public InstalacaoPagtoService(
            IInstalacaoPagtoRepository instalacaoPagtoRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _instalacaoPagtoRepo = instalacaoPagtoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public InstalacaoPagto ObterPorCodigo(int codigo)
        {
            return _instalacaoPagtoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(InstalacaoPagtoParameters parameters)
        {
            var instalacoes = _instalacaoPagtoRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = instalacoes,
                TotalCount = instalacoes.TotalCount,
                CurrentPage = instalacoes.CurrentPage,
                PageSize = instalacoes.PageSize,
                TotalPages = instalacoes.TotalPages,
                HasNext = instalacoes.HasNext,
                HasPrevious = instalacoes.HasPrevious
            };
        }

        public InstalacaoPagto Criar(InstalacaoPagto instalacaoPagto)
        {
            instalacaoPagto.CodInstalPagto = _sequenciaRepo.ObterContador("InstalPagto");
            _instalacaoPagtoRepo.Criar(instalacaoPagto);
            return instalacaoPagto;
        }

        public void Deletar(int codigo)
        {
            _instalacaoPagtoRepo.Deletar(codigo);
        }

        public void Atualizar(InstalacaoPagto instalacaoPagto)
        {
            _instalacaoPagtoRepo.Atualizar(instalacaoPagto);
        }
    }
}
