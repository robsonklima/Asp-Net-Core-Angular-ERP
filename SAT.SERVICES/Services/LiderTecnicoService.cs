using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class LiderTecnicoService : ILiderTecnicoService
    {
        private readonly ILiderTecnicoRepository _liderTecnicoRepo;

        public LiderTecnicoService(ILiderTecnicoRepository liderTecnicoRepo)
        {
            _liderTecnicoRepo = liderTecnicoRepo;
        }

        public void Atualizar(LiderTecnico LiderTecnico)
        {
            this._liderTecnicoRepo.Atualizar(LiderTecnico);
        }

        public LiderTecnico Criar(LiderTecnico liderTecnicoRepo)
        {
            this._liderTecnicoRepo.Criar(liderTecnicoRepo);
            return liderTecnicoRepo;
        }

        public void Deletar(int codigo)
        {
            this._liderTecnicoRepo.Deletar(codigo);
        }

        public LiderTecnico ObterPorCodigo(int codigo)
        {
            return this._liderTecnicoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(LiderTecnicoParameters parameters)
        {
            var liderTecnicos = _liderTecnicoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = liderTecnicos,
                TotalCount = liderTecnicos.TotalCount,
                CurrentPage = liderTecnicos.CurrentPage,
                PageSize = liderTecnicos.PageSize,
                TotalPages = liderTecnicos.TotalPages,
                HasNext = liderTecnicos.HasNext,
                HasPrevious = liderTecnicos.HasPrevious
            };

            return lista;
        }
    }
}
