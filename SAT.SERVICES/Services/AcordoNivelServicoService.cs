using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Services
{
    public class AcordoNivelServicoService
    {
        private readonly IAcordoNivelServicoRepository _ansRepo;

        public AcordoNivelServicoService(IAcordoNivelServicoRepository ansRepo)
        {
            _ansRepo = ansRepo;
        }

        public void Atualizar(AcordoNivelServico ans)
        {
            _ansRepo.Atualizar(ans);
        }

        public AcordoNivelServico Criar(AcordoNivelServico ans)
        {
            _ansRepo.Criar(ans);

            return ans;
        }

        public void Deletar(int codigo)
        {
            _ansRepo.Deletar(codigo);
        }

        public AcordoNivelServico ObterPorCodigo(int codigo)
        {
            return _ansRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(AcordoNivelServicoParameters parameters)
        {
            var anss = _ansRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = anss,
                TotalCount = anss.TotalCount,
                CurrentPage = anss.CurrentPage,
                PageSize = anss.PageSize,
                TotalPages = anss.TotalPages,
                HasNext = anss.HasNext,
                HasPrevious = anss.HasPrevious
            };

            return lista;
        }
    }
}
