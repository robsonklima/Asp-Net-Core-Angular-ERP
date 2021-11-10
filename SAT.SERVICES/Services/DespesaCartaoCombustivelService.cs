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

        public void Atualizar(DespesaCartaoCombustivel despesaCartaoCombustivel)
        {
            throw new System.NotImplementedException();
        }

        public DespesaCartaoCombustivel Criar(DespesaCartaoCombustivel despesaCartaoCombustivel)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public DespesaCartaoCombustivel ObterPorCodigo(int codigo) =>
            _cartaoRepo.ObterPorCodigo(codigo);

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
    }
}
