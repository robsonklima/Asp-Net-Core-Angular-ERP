using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentoOutroServicoService : IOrcamentoOutroServicoService
    {
        private readonly IOrcamentoOutroServicoRepository _orcamentoOutrServRepo;

        public OrcamentoOutroServicoService(IOrcamentoOutroServicoRepository orcamentoOutrServRepo)
        {
            _orcamentoOutrServRepo = orcamentoOutrServRepo;
        }

        public ListViewModel ObterPorParametros(OrcamentoOutroServicoParameters parameters)
        {
            var orcamentos = _orcamentoOutrServRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = orcamentos,
                TotalCount = orcamentos.TotalCount,
                CurrentPage = orcamentos.CurrentPage,
                PageSize = orcamentos.PageSize,
                TotalPages = orcamentos.TotalPages,
                HasNext = orcamentos.HasNext,
                HasPrevious = orcamentos.HasPrevious
            };

            return lista;
        }

        public OrcamentoOutroServico Criar(OrcamentoOutroServico orcamentoOutrServ)
        {
            return _orcamentoOutrServRepo.Criar(orcamentoOutrServ);
        }

        public void Deletar(int codigo)
        {
            _orcamentoOutrServRepo.Deletar(codigo);
        }

        public OrcamentoOutroServico Atualizar(OrcamentoOutroServico orcamentoOutrServ)
        {
            return _orcamentoOutrServRepo.Atualizar(orcamentoOutrServ);
        }

        public OrcamentoOutroServico ObterPorCodigo(int codigo)
        {
            return _orcamentoOutrServRepo.ObterPorCodigo(codigo);
        }
    }
}
