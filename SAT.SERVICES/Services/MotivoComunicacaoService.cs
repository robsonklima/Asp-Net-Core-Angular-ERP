using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class MotivoComunicacaoService : IMotivoComunicacaoService
    {
        private readonly IMotivoComunicacaoRepository _motivoRepo;

        public MotivoComunicacaoService(IMotivoComunicacaoRepository motivoRepo)
        {
            _motivoRepo = motivoRepo;
        }

        public ListViewModel ObterPorParametros(MotivoComunicacaoParameters parameters)
        {
            var regioes = _motivoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = regioes,
                TotalCount = regioes.TotalCount,
                CurrentPage = regioes.CurrentPage,
                PageSize = regioes.PageSize,
                TotalPages = regioes.TotalPages,
                HasNext = regioes.HasNext,
                HasPrevious = regioes.HasPrevious
            };

            return lista;
        }

        public MotivoComunicacao Criar(MotivoComunicacao op)
        {
            _motivoRepo.Criar(op);

            return op;
        }

        public MotivoComunicacao Deletar(int codigo)
        {
            return _motivoRepo.Deletar(codigo);
        }

        public MotivoComunicacao Atualizar(MotivoComunicacao op)
        {
            return _motivoRepo.Atualizar(op);
        }

        public MotivoComunicacao ObterPorCodigo(int codigo)
        {
            return _motivoRepo.ObterPorCodigo(codigo);
        }
    }
}
