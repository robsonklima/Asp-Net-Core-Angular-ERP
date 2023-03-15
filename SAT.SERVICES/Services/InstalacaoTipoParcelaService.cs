using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoTipoParcelaService : IInstalacaoTipoParcelaService
    {
        private readonly IInstalacaoTipoParcelaRepository _instalacaoTipoParcelaRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public InstalacaoTipoParcelaService(
            IInstalacaoTipoParcelaRepository instalacaoTipoParcelaRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _instalacaoTipoParcelaRepo = instalacaoTipoParcelaRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public InstalacaoTipoParcela ObterPorCodigo(int codigo)
        {
            return _instalacaoTipoParcelaRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(InstalacaoTipoParcelaParameters parameters)
        {
            var instalacoes = _instalacaoTipoParcelaRepo.ObterPorParametros(parameters);

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

        public InstalacaoTipoParcela Criar(InstalacaoTipoParcela instalacaoTipoParcela)
        {
            instalacaoTipoParcela.CodInstalTipoParcela = _sequenciaRepo.ObterContador("InstalTipoParcela");
            _instalacaoTipoParcelaRepo.Criar(instalacaoTipoParcela);
            return instalacaoTipoParcela;
        }

        public void Deletar(int codigo)
        {
            _instalacaoTipoParcelaRepo.Deletar(codigo);
        }

        public void Atualizar(InstalacaoTipoParcela instalacaoTipoParcela)
        {
            _instalacaoTipoParcelaRepo.Atualizar(instalacaoTipoParcela);
        }
    }
}
