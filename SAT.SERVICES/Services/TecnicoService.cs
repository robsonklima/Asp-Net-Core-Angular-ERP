using System;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
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

            // if (parameters.PeriodoMediaAtendInicio != DateTime.MinValue && parameters.PeriodoMediaAtendFim != DateTime.MinValue)
            // {
            //     var relatorios = _osRepo
            //         .ObterPorParametros(new OrdemServicoParameters()
            //         {
            //             CodFiliais = parameters.CodFiliais,
            //             DataAberturaInicio = parameters.PeriodoMediaAtendInicio,
            //             DataAberturaFim = parameters.PeriodoMediaAtendFim,
            //             Include = OrdemServicoIncludeEnum.OS_RAT
            //         })
            //         .Where(os => os.RelatoriosAtendimento != null)
            //         .SelectMany(os => os.RelatoriosAtendimento)
            //         .Select(r => new
            //         {
            //             CodTecnico = r.CodTecnico,
            //             DataHoraInicio = r.DataHoraInicio,
            //             DataHoraSolucao = r.DataHoraSolucao
            //         })
            //         .ToList();

            //     foreach (Tecnico tecnico in tecnicos)
            //     {
            //         var qtd = relatorios
            //             .Where(r => r.CodTecnico == tecnico.CodTecnico)
            //             .Count();

            //         var soma = relatorios
            //             .Where(r => r.CodTecnico == tecnico.CodTecnico)
            //             .Sum(r => (r.DataHoraSolucao - r.DataHoraInicio).Minutes);

            //         tecnico.MediaTempoAtendMin = soma / (qtd > 0 ? qtd : 1);
            //     }
            // }

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