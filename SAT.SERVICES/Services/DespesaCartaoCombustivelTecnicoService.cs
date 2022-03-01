using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaCartaoCombustivelTecnicoService : IDespesaCartaoCombustivelTecnicoService
    {
        private readonly IDespesaCartaoCombustivelTecnicoRepository _cartaoControleRepo;

        public DespesaCartaoCombustivelTecnicoService(IDespesaCartaoCombustivelTecnicoRepository cartaoControleRepo)
        {
            _cartaoControleRepo = cartaoControleRepo;
        }

        public void Atualizar(DespesaCartaoCombustivelTecnico despesa)
        {
            _cartaoControleRepo.Atualizar(despesa);
        }

        public DespesaCartaoCombustivelTecnico Criar(DespesaCartaoCombustivelTecnico despesa)
        {
            _cartaoControleRepo.Criar(despesa);

            return despesa;
        }

        public ListViewModel ObterPorParametros(DespesaCartaoCombustivelTecnicoParameters parameters)
        {
            var despesa = _cartaoControleRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = despesa,
                TotalCount = despesa.TotalCount,
                CurrentPage = despesa.CurrentPage,
                PageSize = despesa.PageSize,
                TotalPages = despesa.TotalPages,
                HasNext = despesa.HasNext,
                HasPrevious = despesa.HasPrevious
            };

            return lista;
        }
    }
}
