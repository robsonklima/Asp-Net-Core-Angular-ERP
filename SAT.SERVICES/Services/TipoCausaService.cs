using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TipoCausaService : ITipoCausaService
    {
        private readonly ITipoCausaRepository _tipoCausaRepo;

        public TipoCausaService(ITipoCausaRepository tipoCausaRepo)
        {
            _tipoCausaRepo = tipoCausaRepo;
        }

        public ListViewModel ObterPorParametros(TipoCausaParameters parameters)
        {
            var tiposCausa = _tipoCausaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tiposCausa,
                TotalCount = tiposCausa.TotalCount,
                CurrentPage = tiposCausa.CurrentPage,
                PageSize = tiposCausa.PageSize,
                TotalPages = tiposCausa.TotalPages,
                HasNext = tiposCausa.HasNext,
                HasPrevious = tiposCausa.HasPrevious
            };

            return lista;
        }

        public TipoCausa Criar(TipoCausa tipoCausa)
        {
            _tipoCausaRepo.Criar(tipoCausa);
            return tipoCausa;
        }

        public void Deletar(int codigo)
        {
            _tipoCausaRepo.Deletar(codigo);
        }

        public void Atualizar(TipoCausa tipoCausa)
        {
            _tipoCausaRepo.Atualizar(tipoCausa);
        }

        public TipoCausa ObterPorCodigo(int codigo)
        {
            return _tipoCausaRepo.ObterPorCodigo(codigo);
        }
    }
}
