using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class LocalAtendimentoService : ILocalAtendimentoService
    {
        private readonly ILocalAtendimentoRepository _localRepo;
        private readonly ISequenciaRepository _seqRepo;

        public LocalAtendimentoService(ILocalAtendimentoRepository localRepo, ISequenciaRepository seqRepo)
        {
            _localRepo = localRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(LocalAtendimentoParameters parameters)
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

        public LocalAtendimento Criar(LocalAtendimento localAtendimento)
        {
            localAtendimento.CodPosto = _seqRepo.ObterContador("LocalAtendimento");
            _localRepo.Criar(localAtendimento);
            return localAtendimento;
        }

        public void Deletar(int codigo)
        {
            _localRepo.Deletar(codigo);
        }

        public void Atualizar(LocalAtendimento localAtendimento)
        {
            _localRepo.Atualizar(localAtendimento);
        }

        public LocalAtendimento ObterPorCodigo(int codigo)
        {
            return _localRepo.ObterPorCodigo(codigo);
        }
    }
}
