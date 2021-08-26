using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class CausaService : ICausaService
    {
        private ICausaRepository _causaRepo { get; }

        public CausaService(ICausaRepository causaRepo)
        {
            _causaRepo = causaRepo;
        }

        public ListViewModel ObterPorParametros(CausaParameters parameters)
        {
            var causas = _causaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = causas,
                TotalCount = causas.TotalCount,
                CurrentPage = causas.CurrentPage,
                PageSize = causas.PageSize,
                TotalPages = causas.TotalPages,
                HasNext = causas.HasNext,
                HasPrevious = causas.HasPrevious
            };

            return lista;
        }

        public Causa Criar(Causa causa)
        {
            _causaRepo.Criar(causa);
            return causa;
        }

        public void Deletar(int codigo)
        {
            _causaRepo.Deletar(codigo);
        }

        public void Atualizar(Causa causa)
        {
            _causaRepo.Atualizar(causa);
        }

        public Causa ObterPorCodigo(int codigo)
        {
            return _causaRepo.ObterPorCodigo(codigo);
        }
    }
}
