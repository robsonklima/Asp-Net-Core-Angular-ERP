using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ORSolucaoService : IORSolucaoService
    {
        private readonly IORSolucaoRepository _ORSolucaoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public ORSolucaoService(
            IORSolucaoRepository ORSolucaoRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _ORSolucaoRepo = ORSolucaoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(ORSolucaoParameters parameters)
        {
            var ORes = _ORSolucaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = ORes,
                TotalCount = ORes.TotalCount,
                CurrentPage = ORes.CurrentPage,
                PageSize = ORes.PageSize,
                TotalPages = ORes.TotalPages,
                HasNext = ORes.HasNext,
                HasPrevious = ORes.HasPrevious
            };

            return lista;
        }

        public ORSolucao Criar(ORSolucao orSolucao)
        {
            _ORSolucaoRepo.Criar(orSolucao);

            return orSolucao;
        }

        public void Deletar(int codigo)
        {
            _ORSolucaoRepo.Deletar(codigo);
        }

        public void Atualizar(ORSolucao orSolucao)
        {
            _ORSolucaoRepo.Atualizar(orSolucao);
        }

        public ORSolucao ObterPorCodigo(int codigo)
        {
            return _ORSolucaoRepo.ObterPorCodigo(codigo);
        }
    }
}
