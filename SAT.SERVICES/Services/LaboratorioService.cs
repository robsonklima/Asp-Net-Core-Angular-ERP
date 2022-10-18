using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Views;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class LaboratorioService : ILaboratorioService
    {
        private readonly ILaboratorioRepository _laboratorioRepo;

        public LaboratorioService(ILaboratorioRepository laboratorioRepo)
        {
            _laboratorioRepo = laboratorioRepo;
        }
        public List<ViewLaboratorioTecnicoBancada> ObterTecnicosBancada() =>
            this._laboratorioRepo.ObterTecnicosBancada();
    }
}