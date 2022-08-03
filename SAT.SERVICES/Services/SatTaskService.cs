using System;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class SatTaskService : ISatTaskService
    {
        private readonly ISatTaskRepository _satTaskRepo;
        private readonly ISequenciaRepository _seqRepo;

        public SatTaskService(ISatTaskRepository SatTaskRepo, ISequenciaRepository seqRepo)
        {
            _satTaskRepo = SatTaskRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(SatTaskParameters parameters)
        {
            var SatTasks = _satTaskRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = SatTasks,
                TotalCount = SatTasks.TotalCount,
                CurrentPage = SatTasks.CurrentPage,
                PageSize = SatTasks.PageSize,
                TotalPages = SatTasks.TotalPages,
                HasNext = SatTasks.HasNext,
                HasPrevious = SatTasks.HasPrevious
            };

            return lista;
        }

        public SatTask Criar(SatTask SatTask)
        {
            _satTaskRepo.Criar(SatTask);
            return SatTask;
        }

        public void Deletar(int codigo)
        {
            _satTaskRepo.Deletar(codigo);
        }

        public void Atualizar(SatTask SatTask)
        {
            _satTaskRepo.Atualizar(SatTask);
        }

        public SatTask ObterPorCodigo(int codigo)
        {
            return _satTaskRepo.ObterPorCodigo(codigo);
        }

        public bool PermitirExecucao(SatTaskTipoEnum tipo)
        {
            var task = _satTaskRepo
                .ObterPorParametros(new SatTaskParameters { CodSatTaskTipo = (int)tipo, SortActive = "CodSatTask", SortDirection = "DESC" })
                .FirstOrDefault();

            var is14Horas = (int)DateTime.Now.Hour == 14;
            var is23Horas = (int)DateTime.Now.Hour == 23;
            var isSexta = DateTime.Now.DayOfWeek == DayOfWeek.Friday;
            var isSabado = DateTime.Now.DayOfWeek == DayOfWeek.Saturday;
            var isDomingo = DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
            var isProcessadoHoje = DateTime.Now.Date == task?.DataHoraProcessamento.Date;

            switch (tipo)
            {
                case(SatTaskTipoEnum.PLANTAO_TECNICO_EMAIL):
                    return !isSabado && !isDomingo && is14Horas && !isProcessadoHoje;
                case(SatTaskTipoEnum.INTEGRACAO_FINANCEIRO):
                    return false;
                case(SatTaskTipoEnum.CORRECAO_INTERVALOS_RAT):
                    return !isProcessadoHoje && is23Horas;
                default:
                    return false;
            }
        }
    }
}
