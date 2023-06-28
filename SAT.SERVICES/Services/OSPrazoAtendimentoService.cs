using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OSPrazoAtendimentoService : IOSPrazoAtendimentoService
    {
        private readonly IOSPrazoAtendimentoRepository _OSPrazoAtendimentoRepo;

        public OSPrazoAtendimentoService(IOSPrazoAtendimentoRepository OSPrazoAtendimentoRepo)
        {
            _OSPrazoAtendimentoRepo = OSPrazoAtendimentoRepo;
        }

        public ListViewModel ObterPorParametros(OSPrazoAtendimentoParameters parameters)
        {
            var OSPrazoAtendimentoes = _OSPrazoAtendimentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = OSPrazoAtendimentoes,
                TotalCount = OSPrazoAtendimentoes.TotalCount,
                CurrentPage = OSPrazoAtendimentoes.CurrentPage,
                PageSize = OSPrazoAtendimentoes.PageSize,
                TotalPages = OSPrazoAtendimentoes.TotalPages,
                HasNext = OSPrazoAtendimentoes.HasNext,
                HasPrevious = OSPrazoAtendimentoes.HasPrevious
            };

            return lista;
        }

        public OSPrazoAtendimento Criar(OSPrazoAtendimento p)
        {
            return _OSPrazoAtendimentoRepo.Criar(p);
        }

        public OSPrazoAtendimento Deletar(int codigo)
        {
            return _OSPrazoAtendimentoRepo.Deletar(codigo);
        }

        public OSPrazoAtendimento Atualizar(OSPrazoAtendimento p)
        {
            return _OSPrazoAtendimentoRepo.Atualizar(p);
        }

        public OSPrazoAtendimento ObterPorCodigo(int codigo)
        {
            return _OSPrazoAtendimentoRepo.ObterPorCodigo(codigo);
        }
    }
}
