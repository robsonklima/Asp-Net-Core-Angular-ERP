using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaCartaoCombustivelService : IDespesaCartaoCombustivelService
    {
        private readonly IDespesaCartaoCombustivelRepository _cartaoRepo;

        public DespesaCartaoCombustivelService(IDespesaCartaoCombustivelRepository cartaoRepo)
        {
            _cartaoRepo = cartaoRepo;
        }

        public ListViewModel ObterPorParametros(DespesaCartaoCombustivelParameters parameters)
        {
            var cartaos = _cartaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = cartaos,
                TotalCount = cartaos.TotalCount,
                CurrentPage = cartaos.CurrentPage,
                PageSize = cartaos.PageSize,
                TotalPages = cartaos.TotalPages,
                HasNext = cartaos.HasNext,
                HasPrevious = cartaos.HasPrevious
            };

            return lista;
        }

        public DespesaCartaoCombustivel Criar(DespesaCartaoCombustivel despesaCartaoCombustivel)
        {
            //_cartaoRepo.Criar(despesaCartaoCombustivel);
            return despesaCartaoCombustivel;
        }

        public void Deletar(int codigo)
        {
            //_cartaoRepo.Deletar(codigo);
        }

        public void Atualizar(DespesaCartaoCombustivel despesaCartaoCombustivel)
        {
            //_cartaoRepo.Atualizar(DespesaCartaoCombustivel);
        }

        public DespesaCartaoCombustivel ObterPorCodigo(int codigo)
        {
            return null; //_cartaoRepo.ObterPorCodigo(codigo);
        }
    }
}
