using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class LocalEnvioNFFaturamentoVinculadoService : ILocalEnvioNFFaturamentoVinculadoService
    {
        private readonly ILocalEnvioNFFaturamentoVinculadoRepository _localRepo;
        private readonly ISequenciaRepository _seqRepo;

        public LocalEnvioNFFaturamentoVinculadoService(ILocalEnvioNFFaturamentoVinculadoRepository localRepo, ISequenciaRepository seqRepo)
        {
            _localRepo = localRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(LocalEnvioNFFaturamentoVinculadoParameters parameters)
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

        public LocalEnvioNFFaturamentoVinculado Criar(LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado)
        {
            localEnvioNFFaturamentoVinculado.CodLocalEnvioNFFaturamento = _seqRepo.ObterContador("LocalEnvioNFFaturamentoVinculado");
            _localRepo.Criar(localEnvioNFFaturamentoVinculado);
            return localEnvioNFFaturamentoVinculado;
        }

        public void Deletar(int codigo)
        {
            _localRepo.Deletar(codigo);
        }

        public void Atualizar(LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado)
        {
            _localRepo.Atualizar(localEnvioNFFaturamentoVinculado);
        }

        public LocalEnvioNFFaturamentoVinculado ObterPorCodigo(int codLocalEnvioNFFaturamento, int codPosto, int codContrato)
        {
            return _localRepo.ObterPorCodigo(codLocalEnvioNFFaturamento, codPosto, codContrato);
        }
    }
}
