using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoService : IInstalacaoService
    {
        private readonly IInstalacaoRepository _instalacaoRepo;

        public InstalacaoService(IInstalacaoRepository instalacaoRepo)
        {
            _instalacaoRepo = instalacaoRepo;
        }

        public ListViewModel ObterPorParametros(InstalacaoParameters parameters)
        {
            var instalacao = _instalacaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = instalacao,
                TotalCount = instalacao.TotalCount,
                CurrentPage = instalacao.CurrentPage,
                PageSize = instalacao.PageSize,
                TotalPages = instalacao.TotalPages,
                HasNext = instalacao.HasNext,
                HasPrevious = instalacao.HasPrevious
            };

            return lista;
        }

        public Instalacao Criar(Instalacao instalacao)
        {
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
