using SAT.MODELS.Entities.Params;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PecaStatusService : IPecaStatusService
    {
        private readonly IPecaStatusRepository _pecaRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public PecaStatusService(IPecaStatusRepository pecaRepo, ISequenciaRepository sequenciaRepo)
        {
            _pecaRepo = pecaRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(PecaStatusParameters parameters)
        {
            var pecas = _pecaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = pecas,
                TotalCount = pecas.TotalCount,
                CurrentPage = pecas.CurrentPage,
                PageSize = pecas.PageSize,
                TotalPages = pecas.TotalPages,
                HasNext = pecas.HasNext,
                HasPrevious = pecas.HasPrevious
            };

            return lista;
        }

        public PecaStatus Criar(PecaStatus pecaStatus)
        {
            pecaStatus.CodPecaStatus = _sequenciaRepo.ObterContador("Peca");
            _pecaRepo.Criar(pecaStatus);
            return pecaStatus;
        }

        public void Deletar(int codigo) =>
            _pecaRepo.Deletar(codigo);

        public void Atualizar(PecaStatus pecaStatus) =>
            _pecaRepo.Atualizar(pecaStatus);

        public PecaStatus ObterPorCodigo(int codigo) =>
            _pecaRepo.ObterPorCodigo(codigo);

        public IActionResult ExportToExcel(PecaStatusParameters parameters)
        {
            var os = _pecaRepo.ObterPorParametros(parameters);

            
            return null; //new BaseExcelService<Peca>().CreateWorkbook(os.Cast<Peca>().ToList());
        }
    }
}