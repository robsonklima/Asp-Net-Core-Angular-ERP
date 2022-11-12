using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Views;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class BancadaLaboratorioService : IBancadaLaboratorioService
    {
        private readonly IBancadaLaboratorioRepository _bancadaLaboratorioRepo;

        public BancadaLaboratorioService(IBancadaLaboratorioRepository BancadaLaboratorioRepo)
        {
            _bancadaLaboratorioRepo = BancadaLaboratorioRepo;
        }

        public ListViewModel ObterPorParametros(BancadaLaboratorioParameters parameters)
        {
            var labs = _bancadaLaboratorioRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = labs,
                TotalCount = labs.TotalCount,
                CurrentPage = labs.CurrentPage,
                PageSize = labs.PageSize,
                TotalPages = labs.TotalPages,
                HasNext = labs.HasNext,
                HasPrevious = labs.HasPrevious
            };

            return lista;
        }

        public BancadaLaboratorio Criar(BancadaLaboratorio BancadaLaboratorio)
        {
            _bancadaLaboratorioRepo.Criar(BancadaLaboratorio);
            return BancadaLaboratorio;
        }

        public void Deletar(int codigo)
        {
            _bancadaLaboratorioRepo.Deletar(codigo);
        }

        public void Atualizar(BancadaLaboratorio BancadaLaboratorio)
        {
            _bancadaLaboratorioRepo.Atualizar(BancadaLaboratorio);
        }

        public BancadaLaboratorio ObterPorCodigo(int num)
        {
            return _bancadaLaboratorioRepo.ObterPorCodigo(num);
        }

        public List<ViewLaboratorioTecnicoBancada> ObterPorView(BancadaLaboratorioParameters parameters) =>
            this._bancadaLaboratorioRepo.ObterPorView(parameters);
    }
}
