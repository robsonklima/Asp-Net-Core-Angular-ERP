using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class FerramentaTecnicoService : IFerramentaTecnicoService
    {
        private readonly IFerramentaTecnicoRepository _ferramentaTecnicoRepo;

        public FerramentaTecnicoService(IFerramentaTecnicoRepository ferramentaTecnicoRepo)
        {
            _ferramentaTecnicoRepo = ferramentaTecnicoRepo;
        }

        public void Atualizar(FerramentaTecnico ferramentaTecnico)
        {
            this._ferramentaTecnicoRepo.Atualizar(ferramentaTecnico);
        }

        public FerramentaTecnico Criar(FerramentaTecnico ferramentaTecnico)
        {
            this._ferramentaTecnicoRepo.Criar(ferramentaTecnico);
            return ferramentaTecnico;
        }

        public void Deletar(int codigo)
        {
            this._ferramentaTecnicoRepo.Deletar(codigo);
        }

        public FerramentaTecnico ObterPorCodigo(int codigo)
        {
            return this._ferramentaTecnicoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(FerramentaTecnicoParameters parameters)
        {
            var ferramentaTecnicos = _ferramentaTecnicoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ferramentaTecnicos,
                TotalCount = ferramentaTecnicos.TotalCount,
                CurrentPage = ferramentaTecnicos.CurrentPage,
                PageSize = ferramentaTecnicos.PageSize,
                TotalPages = ferramentaTecnicos.TotalPages,
                HasNext = ferramentaTecnicos.HasNext,
                HasPrevious = ferramentaTecnicos.HasPrevious
            };

            return lista;
        }
    }
}
