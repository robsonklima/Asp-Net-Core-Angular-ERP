using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TipoServicoService : ITipoServicoService
    {
        private readonly ITipoServicoRepository _tipoServicoRepo;

        public TipoServicoService(ITipoServicoRepository tipoServicoRepo)
        {
            _tipoServicoRepo = tipoServicoRepo;
        }

        public ListViewModel ObterPorParametros(TipoServicoParameters parameters)
        {
            var tiposServico = _tipoServicoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tiposServico,
                TotalCount = tiposServico.TotalCount,
                CurrentPage = tiposServico.CurrentPage,
                PageSize = tiposServico.PageSize,
                TotalPages = tiposServico.TotalPages,
                HasNext = tiposServico.HasNext,
                HasPrevious = tiposServico.HasPrevious
            };

            return lista;
        }

        public TipoServico Criar(TipoServico tipoServico)
        {
            _tipoServicoRepo.Criar(tipoServico);
            return tipoServico;
        }

        public void Deletar(int codigo)
        {
            _tipoServicoRepo.Deletar(codigo);
        }

        public void Atualizar(TipoServico tipoServico)
        {
            _tipoServicoRepo.Atualizar(tipoServico);
        }

        public TipoServico ObterPorCodigo(int codigo)
        {
            return _tipoServicoRepo.ObterPorCodigo(codigo);
        }
    }
}
