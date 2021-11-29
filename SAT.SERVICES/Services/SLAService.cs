using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public class SLAService : ISLAService
    {
        private readonly ISLARepository _SLARepo;

        public SLAService(ISLARepository slaRepo)
        {
            _SLARepo = slaRepo;
        }

        public ListViewModel ObterPorParametros(SLAParameters parameters)
        {
            var contratos = _SLARepo.ObterPorParametros(parameters);

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

        public SLA Criar(SLA sla)
        {

            _SLARepo.Criar(sla);

            return sla;
        }

        public void Deletar(int codigo)
        {
            _SLARepo.Deletar(codigo);
        }

        public SLA Atualizar(SLA sla)
        {
            _SLARepo.Atualizar(sla);

            return sla;
        }

        public SLA ObterPorCodigo(int codigo)
        {
            return _SLARepo.ObterPorCodigo(codigo);
        }

    }
}
