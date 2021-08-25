using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoService : IOrdemServicoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public OrdemServicoService(IOrdemServicoRepository ordemServicoRepo, ISequenciaRepository sequenciaRepo)
        {
            _ordemServicoRepo = ordemServicoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public void Atualizar(OrdemServico ordemServico)
        {
            _ordemServicoRepo.Atualizar(ordemServico);
        }

        public void Criar(OrdemServico ordemServico)
        {
            ordemServico.CodOS = _sequenciaRepo.ObterContador(Constants.TABELA_ORDEM_SERVICO);

            _ordemServicoRepo.Criar(ordemServico);
        }

        public void Deletar(int codOS)
        {
            _ordemServicoRepo.Deletar(codOS);
        }

        public OrdemServico ObterPorCodigo(int codigo)
        {
            return _ordemServicoRepo.ObterPorCodigo(codigo);
        }

        public PagedList<OrdemServico> ObterPorParametros(OrdemServicoParameters parameters)
        {
            return _ordemServicoRepo.ObterPorParametros(parameters);
        }

        public IEnumerable<OrdemServico> ObterTodos()
        {
            return _ordemServicoRepo.ObterTodos();
        }
    }
}
