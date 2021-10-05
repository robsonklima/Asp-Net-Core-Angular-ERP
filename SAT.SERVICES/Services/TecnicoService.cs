using System;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class TecnicoService : ITecnicoService
    {
        private readonly ITecnicoRepository _tecnicosRepo;
        private readonly IOrdemServicoRepository _osRepo;
        private readonly ISequenciaRepository _seqRepo;

        public TecnicoService(
            ITecnicoRepository tecnicosRepo, 
            ISequenciaRepository seqRepo,
            IOrdemServicoRepository osRepo
        )
        {
            _tecnicosRepo = tecnicosRepo;
            _osRepo = osRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(TecnicoParameters parameters)
        {
            var tecnicos = _tecnicosRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tecnicos,
                TotalCount = tecnicos.TotalCount,
                CurrentPage = tecnicos.CurrentPage,
                PageSize = tecnicos.PageSize,
                TotalPages = tecnicos.TotalPages,
                HasNext = tecnicos.HasNext,
                HasPrevious = tecnicos.HasPrevious
            };

            var primeroDiaDoMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var ultimoDiaDoMes = primeroDiaDoMes.AddMonths(1);

            var relatorios = _osRepo
                .ObterPorParametros(new OrdemServicoParameters() {
                    CodFiliais = "4",
                    DataAberturaInicio = primeroDiaDoMes,
                    DataAberturaFim = ultimoDiaDoMes,
                })
                .Where(os => os.RelatoriosAtendimento != null)
                .SelectMany(os => os.RelatoriosAtendimento)
                .Select(r => new { 
                    CodTecnico = r.CodTecnico, 
                    DataHoraInicio = r.DataHoraInicio, 
                    DataHoraSolucao = r.DataHoraSolucao
                })
                .ToList();

            foreach(Tecnico tecnico in lista.Items)
            {
                var qtd = relatorios
                    .Where(r => r.CodTecnico == tecnico.CodTecnico)
                    .Count();

                var soma = relatorios
                    .Where(r => r.CodTecnico == tecnico.CodTecnico)
                    .Sum(r => (r.DataHoraSolucao - r.DataHoraInicio).Minutes);

                tecnico.MediaTempoAtendMinutosUlt30Dias = soma / (qtd > 0 ? qtd : 1);
            }

            return lista;
        }

        public Tecnico Criar(Tecnico tecnico)
        {
            tecnico.CodTecnico = _seqRepo.ObterContador("Tecnico");
            _tecnicosRepo.Criar(tecnico);
            return tecnico;
        }

        public void Deletar(int codigo)
        {
            _tecnicosRepo.Deletar(codigo);
        }

        public void Atualizar(Tecnico tecnico)
        {
            _tecnicosRepo.Atualizar(tecnico);
        }

        public Tecnico ObterPorCodigo(int codigo)
        {
            return _tecnicosRepo.ObterPorCodigo(codigo);
        }
    }
}