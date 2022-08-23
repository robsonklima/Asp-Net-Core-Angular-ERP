using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class MensagemTecnicoService : IMensagemTecnicoService
    {
        private readonly IMensagemTecnicoRepository _mensagemTecnicoRepo;

        public MensagemTecnicoService(IMensagemTecnicoRepository mensagemTecnicoRepo)
        {
            _mensagemTecnicoRepo = mensagemTecnicoRepo;
        }

        public ListViewModel ObterPorParametros(MensagemTecnicoParameters parameters)
        {
            var MensagemTecnicoes = _mensagemTecnicoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = MensagemTecnicoes,
                TotalCount = MensagemTecnicoes.TotalCount,
                CurrentPage = MensagemTecnicoes.CurrentPage,
                PageSize = MensagemTecnicoes.PageSize,
                TotalPages = MensagemTecnicoes.TotalPages,
                HasNext = MensagemTecnicoes.HasNext,
                HasPrevious = MensagemTecnicoes.HasPrevious
            };

            return lista;
        }

        public void Criar(MensagemTecnico msg)
        {
            _mensagemTecnicoRepo.Criar(msg);
        }

        public void Deletar(int codigo)
        {
            _mensagemTecnicoRepo.Deletar(codigo);
        }

        public void Atualizar(MensagemTecnico MensagemTecnico)
        {
            _mensagemTecnicoRepo.Atualizar(MensagemTecnico);
        }

        public MensagemTecnico ObterPorCodigo(int codigo)
        {
            return _mensagemTecnicoRepo.ObterPorCodigo(codigo);
        }

        MensagemTecnico IMensagemTecnicoService.Atualizar(MensagemTecnico msg)
        {
            throw new System.NotImplementedException();
        }
    }
}
