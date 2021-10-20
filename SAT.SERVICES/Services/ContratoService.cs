using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public class ContratoService : IContratoService
    {
        private readonly IContratoRepository _contratoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public ContratoService(
            IContratoRepository contratoRepo,
            ISequenciaRepository sequenciaRepo
        )
        {
            _contratoRepo = contratoRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(ContratoParameters parameters)
        {
            var contratos = _contratoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = contratos,
                TotalCount = contratos.TotalCount,
                CurrentPage = contratos.CurrentPage,
                PageSize = contratos.PageSize,
                TotalPages = contratos.TotalPages,
                HasNext = contratos.HasNext,
                HasPrevious = contratos.HasPrevious
            };

            return lista;
        }

        public Contrato Criar(Contrato contrato)
        {
            contrato.CodContrato = _sequenciaRepo.ObterContador("Contrato");
            
            _contratoRepo.Criar(contrato);
            
            return contrato;
        }

        public void Deletar(int codigo)
        {
            _contratoRepo.Deletar(codigo);
        }

        public void Atualizar(Contrato contrato)
        {
            _contratoRepo.Atualizar(contrato);
        }

        public Contrato ObterPorCodigo(int codigo)
        {
            return _contratoRepo.ObterPorCodigo(codigo);
        }

        public IActionResult ExportToExcel(ContratoParameters parameters)
        {
            var os = _contratoRepo.ObterPorParametros(parameters);
            return new BaseExcelService<Contrato>().CreateWorkbook(os.Cast<Contrato>().ToList());
        }
    }
}
