using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AcaoComponenteService : IAcaoComponenteService
    {
        private readonly IAcaoComponenteRepository _acaoComponenteRepo;

        public AcaoComponenteService(IAcaoComponenteRepository acaoComponenteRepo)
        {
            _acaoComponenteRepo = acaoComponenteRepo;
        }

        /// <summary>
        /// Acao componente é sempre vai atualizar o registro, raramente adiciona o que não existe
        /// </summary>
        /// <param name="acao"></param>
        public void Atualizar(AcaoComponente acao)
        {
            if (_acaoComponenteRepo.ExisteAcaoComponente(acao).CodAcaoComponente != 0)
            {
                _acaoComponenteRepo.Atualizar(acao);
            }
            else
            {
                _acaoComponenteRepo.Criar(acao);
            }
        }

        public AcaoComponente Criar(AcaoComponente acao)
        {
            _acaoComponenteRepo.Criar(acao);
            return acao;
        }

        public void Deletar(int codigo)
        {
            _acaoComponenteRepo.Deletar(codigo);
        }

        public AcaoComponente ObterPorCodigo(int codigo)
        {
            return _acaoComponenteRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(AcaoComponenteParameters parameters)
        {
            var acoes = _acaoComponenteRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = acoes,
                TotalCount = acoes.TotalCount,
                CurrentPage = acoes.CurrentPage,
                PageSize = acoes.PageSize,
                TotalPages = acoes.TotalPages,
                HasNext = acoes.HasNext,
                HasPrevious = acoes.HasPrevious
            };

            return lista;
        }
    }
}
