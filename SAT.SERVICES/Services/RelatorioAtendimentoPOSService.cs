using System;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RelatorioAtendimentoPOSService : IRelatorioAtendimentoPOSService
    {
        private readonly IRelatorioAtendimentoPOSRepository _relatorioAtendimentoPOSRepo;

        public RelatorioAtendimentoPOSService(IRelatorioAtendimentoPOSRepository relatorioAtendimentoPOSRepo)
        {
            _relatorioAtendimentoPOSRepo = relatorioAtendimentoPOSRepo;
        }

        public ListViewModel ObterPorParametros(RelatorioAtendimentoPOSParameters parameters)
        {
            var RelatorioAtendimentoPOSs = _relatorioAtendimentoPOSRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = RelatorioAtendimentoPOSs,
                TotalCount = RelatorioAtendimentoPOSs.TotalCount,
                CurrentPage = RelatorioAtendimentoPOSs.CurrentPage,
                PageSize = RelatorioAtendimentoPOSs.PageSize,
                TotalPages = RelatorioAtendimentoPOSs.TotalPages,
                HasNext = RelatorioAtendimentoPOSs.HasNext,
                HasPrevious = RelatorioAtendimentoPOSs.HasPrevious
            };

            return lista;
        }

        public RelatorioAtendimentoPOS Criar(RelatorioAtendimentoPOS relatorio)
        {
            _relatorioAtendimentoPOSRepo.Criar(relatorio);
            return relatorio;
        }

        public RelatorioAtendimentoPOS Deletar(int codigo)
        {
            return _relatorioAtendimentoPOSRepo.Deletar(codigo);
        }

        public RelatorioAtendimentoPOS Atualizar(RelatorioAtendimentoPOS relatorio)
        {
            return _relatorioAtendimentoPOSRepo.Atualizar(relatorio);
        }

        public RelatorioAtendimentoPOS ObterPorCodigo(int codigo)
        {
            return _relatorioAtendimentoPOSRepo.ObterPorCodigo(codigo);
        }
    }
}
