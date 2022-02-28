using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class LocalEnvioNFFaturamentoService : ILocalEnvioNFFaturamentoService
    {
        private readonly ILocalEnvioNFFaturamentoRepository _localRepo;
        private readonly ISequenciaRepository _seqRepo;

        public LocalEnvioNFFaturamentoService(ILocalEnvioNFFaturamentoRepository localRepo, ISequenciaRepository seqRepo)
        {
            _localRepo = localRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(LocalEnvioNFFaturamentoParameters parameters)
        {
            var locais = _localRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = locais,
                TotalCount = locais.TotalCount,
                CurrentPage = locais.CurrentPage,
                PageSize = locais.PageSize,
                TotalPages = locais.TotalPages,
                HasNext = locais.HasNext,
                HasPrevious = locais.HasPrevious
            };

            return lista;
        }

        public LocalEnvioNFFaturamento Criar(LocalEnvioNFFaturamento localEnvioNFFaturamento)
        {
            localEnvioNFFaturamento.CodLocalEnvioNFFaturamento = _seqRepo.ObterContador("LocalEnvioNFFaturamento");
            _localRepo.Criar(localEnvioNFFaturamento);
            return localEnvioNFFaturamento;
        }

        public void Deletar(int codigo)
        {
            _localRepo.Deletar(codigo);
        }

        public void Atualizar(LocalEnvioNFFaturamento localEnvioNFFaturamento)
        {
            _localRepo.Atualizar(localEnvioNFFaturamento);
        }

        public LocalEnvioNFFaturamento ObterPorCodigo(int codigo)
        {
            return _localRepo.ObterPorCodigo(codigo);
        }
    }
}
