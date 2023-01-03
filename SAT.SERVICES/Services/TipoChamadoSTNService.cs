using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TipoChamadoSTNService : ITipoChamadoSTNService
    {
        private readonly ITipoChamadoSTNRepository _tipoChamadoSTNRepo;

        public TipoChamadoSTNService(
            ITipoChamadoSTNRepository tipoChamadoSTNRepo
        )
        {
            _tipoChamadoSTNRepo = tipoChamadoSTNRepo;
        }

        public void Atualizar(TipoChamadoSTN tipoChamadoSTN)
        {
            _tipoChamadoSTNRepo.Atualizar(tipoChamadoSTN);
        }

        public void Criar(TipoChamadoSTN tipoChamadoSTN)
        {
            _tipoChamadoSTNRepo.Criar(tipoChamadoSTN);
        }

        public void Deletar(int codigoTipoChamadoSTN)
        {
            _tipoChamadoSTNRepo.Deletar(codigoTipoChamadoSTN);
        }

        public TipoChamadoSTN ObterPorCodigo(int codTipoChamadoSTN)
        {
            return _tipoChamadoSTNRepo.ObterPorCodigo(codTipoChamadoSTN);
        }

        public ListViewModel ObterPorParametros(TipoChamadoSTNParameters parameters)
        {
            var tipoChamadoSTNs = _tipoChamadoSTNRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tipoChamadoSTNs,
                TotalCount = tipoChamadoSTNs.TotalCount,
                CurrentPage = tipoChamadoSTNs.CurrentPage,
                PageSize = tipoChamadoSTNs.PageSize,
                TotalPages = tipoChamadoSTNs.TotalPages,
                HasNext = tipoChamadoSTNs.HasNext,
                HasPrevious = tipoChamadoSTNs.HasPrevious
            };

            return lista;
        }

    }
}
