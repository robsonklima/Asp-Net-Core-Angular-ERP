using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.SERVICES.Services
{
    public class FeriadoService : IFeriadoService
    {
        private readonly IFeriadoRepository _feriadoRepo;

        public FeriadoService(IFeriadoRepository feriadoRepo)
        {
            _feriadoRepo = feriadoRepo;
        }

        public ListViewModel ObterPorParametros(FeriadoParameters parameters)
        {
            var feriados = _feriadoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = feriados,
                TotalCount = feriados.TotalCount,
                CurrentPage = feriados.CurrentPage,
                PageSize = feriados.PageSize,
                TotalPages = feriados.TotalPages,
                HasNext = feriados.HasNext,
                HasPrevious = feriados.HasPrevious
            };

            return lista;
        }

        public Feriado Criar(Feriado feriado)
        {
            _feriadoRepo.Criar(feriado);
            return feriado;
        }

        public void Deletar(int codigo)
        {
            _feriadoRepo.Deletar(codigo);
        }

        public void Atualizar(Feriado feriado)
        {
            _feriadoRepo.Atualizar(feriado);
        }

        public Feriado ObterPorCodigo(int codigo)
        {
            return _feriadoRepo.ObterPorCodigo(codigo);
        }
    }
}
