using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DefeitoComponenteService : IDefeitoComponenteService
    {
        private readonly IDefeitoComponenteRepository _defeitoComponenteRepo;

        public DefeitoComponenteService(IDefeitoComponenteRepository defeitoComponenteRepo)
        {
            _defeitoComponenteRepo = defeitoComponenteRepo;
        }

        /// <summary>
        /// Defeito componente é sempre vai atualizar o registro, raramente adiciona o que não existe
        /// </summary>
        /// <param name="defeito"></param>
        public void Atualizar(DefeitoComponente defeito)
        {
            if (_defeitoComponenteRepo.ExisteDefeitoComponente(defeito).CodDefeitoComponente != 0)
            {
                _defeitoComponenteRepo.Atualizar(defeito);
            }
            else
            {
                _defeitoComponenteRepo.Criar(defeito);
            }
        }

        public DefeitoComponente Criar(DefeitoComponente defeito)
        {
            _defeitoComponenteRepo.Criar(defeito);
            return defeito;
        }

        public void Deletar(int codigo)
        {
            _defeitoComponenteRepo.Deletar(codigo);
        }

        public DefeitoComponente ObterPorCodigo(int codigo)
        {
            return _defeitoComponenteRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DefeitoComponenteParameters parameters)
        {
            var acoes = _defeitoComponenteRepo.ObterPorParametros(parameters);

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
