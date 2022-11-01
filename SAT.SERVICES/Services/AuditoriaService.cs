using System;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly IAuditoriaRepository _auditoriaRepo;
        private readonly IDespesaPeriodoTecnicoRepository _despesaPeriodoTecnicoRepo;

        public AuditoriaService(
            IAuditoriaRepository auditoriaRepo,
            IDespesaPeriodoTecnicoRepository despesaPeriodoTecnicoRepo
        )
        {
            _auditoriaRepo = auditoriaRepo;
            _despesaPeriodoTecnicoRepo = despesaPeriodoTecnicoRepo;
        }

        public void Atualizar(Auditoria auditoria)
        {
            _auditoriaRepo.Atualizar(auditoria);
        }

        public void Criar(Auditoria auditoria)
        {
            _auditoriaRepo.Criar(auditoria);
        }

        public void Deletar(int codigoAuditoria)
        {
            _auditoriaRepo.Deletar(codigoAuditoria);
        }

        public Auditoria ObterPorCodigo(int codAuditoria)
        {
            return _auditoriaRepo.ObterPorCodigo(codAuditoria);
        }

        public ListViewModel ObterPorParametros(AuditoriaParameters parameters)
        {
            var auditorias = _auditoriaRepo.ObterPorParametros(parameters);

            for (int i = 0; i < auditorias.Count; i++)
            {
                 if (auditorias[i].CodAuditoriaStatus == (int)AuditoriaStatusEnum.FINALIZADO && auditorias[i].CodAuditoriaVeiculo == null)
                    {
                         auditorias[i].QtdDespesasPendentes = ConsultarDespesasPendentes(auditorias[i]);
                    }else {auditorias[i].QtdDespesasPendentes = 0;}
            }

            var lista = new ListViewModel
            {
                Items = auditorias,
                TotalCount = auditorias.TotalCount,
                CurrentPage = auditorias.CurrentPage,
                PageSize = auditorias.PageSize,
                TotalPages = auditorias.TotalPages,
                HasNext = auditorias.HasNext,
                HasPrevious = auditorias.HasPrevious
            };

            return lista;
        }

        public ListViewModel ObterPorView(AuditoriaParameters parameters)
        {
            var auditorias = _auditoriaRepo.ObterPorView(parameters);

            var lista = new ListViewModel
            {
                Items = auditorias,
                TotalCount = auditorias.TotalCount,
                CurrentPage = auditorias.CurrentPage,
                PageSize = auditorias.PageSize,
                TotalPages = auditorias.TotalPages,
                HasNext = auditorias.HasNext,
                HasPrevious = auditorias.HasPrevious
            };

            return lista;
        }

        private int ConsultarDespesasPendentes(Auditoria auditoria) {

            var despesasPendentes = _despesaPeriodoTecnicoRepo.ObterPorParametros(new DespesaPeriodoTecnicoParameters {
                CodDespesaPeriodoStatusNotIn = "2",                
                FimPeriodo = auditoria.DataHoraCad.Value.AddDays(-30),
                CodTecnico = auditoria?.Usuario?.CodTecnico.ToString()
            });
          

            return despesasPendentes.Count;

        }

    }
}
