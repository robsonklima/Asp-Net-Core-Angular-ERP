using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class FilialService : IFilialService
    {
        private readonly IFilialRepository _filialRepo;

        public FilialService(IFilialRepository filialRepo)
        {
            _filialRepo = filialRepo;
        }

        public ListViewModel ObterPorParametros(FilialParameters parameters)
        {
            var filiais = _filialRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = filiais,
                TotalCount = filiais.TotalCount,
                CurrentPage = filiais.CurrentPage,
                PageSize = filiais.PageSize,
                TotalPages = filiais.TotalPages,
                HasNext = filiais.HasNext,
                HasPrevious = filiais.HasPrevious
            };
        }

        public Filial Criar(Filial filial)
        {
            _filialRepo.Criar(filial);
            return filial;
        }

        public void Deletar(int codigo)
        {
            _filialRepo.Deletar(codigo);
        }

        public void Atualizar(Filial filial)
        {
            _filialRepo.Atualizar(filial);
        }

        public Filial ObterPorCodigo(int codigo)
        {
            return _filialRepo.ObterPorCodigo(codigo);
        }
    }
}
