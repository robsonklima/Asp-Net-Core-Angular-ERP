using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

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

        public OrdemServico Atualizar(OrdemServico ordemServico)
        {
            _ordemServicoRepo.Atualizar(ordemServico);

            return ordemServico;
        }

        public OrdemServico Criar(OrdemServico ordemServico)
        {
            ordemServico.CodOS = _sequenciaRepo.ObterContador("OS");

            _ordemServicoRepo.Criar(ordemServico);

            return ordemServico;
        }

        public void Deletar(int codOS)
        {
            _ordemServicoRepo.Deletar(codOS);
        }

        public OrdemServico ObterPorCodigo(int codigo)
        {
            var os = _ordemServicoRepo.ObterPorCodigo(codigo);
            os.Alertas = ObterAlertas(os.CodOS);
            return os;
        }

        public ListViewModel ObterPorParametros(OrdemServicoParameters parameters)
        {
            var ordensServico = _ordemServicoRepo.ObterPorParametros(parameters);
            
            var lista = new ListViewModel
            {
                Items = ordensServico,
                TotalCount = ordensServico.TotalCount,
                CurrentPage = ordensServico.CurrentPage,
                PageSize = ordensServico.PageSize,
                TotalPages = ordensServico.TotalPages,
                HasNext = ordensServico.HasNext,
                HasPrevious = ordensServico.HasPrevious
            };

            return lista;
        }

        private List<Alerta> ObterAlertas(int codos) {
            List<Alerta> Alertas = new List<Alerta>();
            Alertas.Add(new Alerta() {
                Tipo="ALERTA_1",
                Titulo="Chamado Bloquio STN",
                Descricao= "Chamado bloqueado devido a...."
            });

            return Alertas;
        }
    }
}
