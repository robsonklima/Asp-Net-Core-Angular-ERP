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

           return new ListViewModel
            {
                Items = tecnicos,
                TotalCount = tecnicos.TotalCount,
                CurrentPage = tecnicos.CurrentPage,
                PageSize = tecnicos.PageSize,
                TotalPages = tecnicos.TotalPages,
                HasNext = tecnicos.HasNext,
                HasPrevious = tecnicos.HasPrevious
            };
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