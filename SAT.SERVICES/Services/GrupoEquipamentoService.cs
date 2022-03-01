using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class GrupoEquipamentoService : IGrupoEquipamentoService
    {
        private readonly IGrupoEquipamentoRepository _grupoEquipamentoRepo;
        private readonly ISequenciaRepository _seqRepo;

        public GrupoEquipamentoService(IGrupoEquipamentoRepository grupoEquipamentoRepo, ISequenciaRepository seqRepo)
        {
            _grupoEquipamentoRepo = grupoEquipamentoRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(GrupoEquipamentoParameters parameters)
        {
            var grupoEquipamentos = _grupoEquipamentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = grupoEquipamentos,
                TotalCount = grupoEquipamentos.TotalCount,
                CurrentPage = grupoEquipamentos.CurrentPage,
                PageSize = grupoEquipamentos.PageSize,
                TotalPages = grupoEquipamentos.TotalPages,
                HasNext = grupoEquipamentos.HasNext,
                HasPrevious = grupoEquipamentos.HasPrevious
            };

            return lista;
        }

        public GrupoEquipamento Criar(GrupoEquipamento grupoEquipamento)
        {
            grupoEquipamento.CodGrupoEquip = _seqRepo.ObterContador("GrupoEquipamento"); ;
            _grupoEquipamentoRepo.Criar(grupoEquipamento: grupoEquipamento);
            return grupoEquipamento;
        }

        public void Deletar(int codigo)
        {
            _grupoEquipamentoRepo.Deletar(codigo);
        }

        public void Atualizar(GrupoEquipamento grupoEquipamento)
        {
            _grupoEquipamentoRepo.Atualizar(grupoEquipamento);
        }

        public GrupoEquipamento ObterPorCodigo(int codigo)
        {
            return _grupoEquipamentoRepo.ObterPorCodigo(codigo);
        }
    }
}
