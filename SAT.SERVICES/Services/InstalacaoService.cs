using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoService : IInstalacaoService
    {
        private readonly IInstalacaoRepository _instalacaoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public InstalacaoService(
            IInstalacaoRepository instalacaoRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _instalacaoRepo = instalacaoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(InstalacaoParameters parameters)
        {
            var instalacoes = _instalacaoRepo.ObterPorParametros(parameters);

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

        public Instalacao Criar(Instalacao instalacao)
        {
            instalacao.CodInstalacao = _sequenciaRepo.ObterContador("Instalacao");
            _instalacaoRepo.Criar(instalacao);
            return instalacao;
        }

        public void Deletar(int codigo)
        {
            _instalacaoRepo.Deletar(codigo);
        }

        public void Atualizar(Instalacao instalacao)
        {
            _instalacaoRepo.Atualizar(instalacao);
        }

        public Instalacao ObterPorCodigo(int codigo)
        {
            return _instalacaoRepo.ObterPorCodigo(codigo);
        }
    }
}
