using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class ContratoEquipamentoDataRepository : IContratoEquipamentoDataRepository
    {
        private readonly AppDbContext _context;

        public ContratoEquipamentoDataRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ContratoEquipamentoData contrato)
        {
            ContratoEquipamentoData c = _context.ContratoEquipamentoData.FirstOrDefault(d => d.CodContratoEquipData == contrato.CodContratoEquipData);
            
            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(contrato);
                _context.ChangeTracker.Clear();
                _context.SaveChanges();
            }
        }

        public void Criar(ContratoEquipamentoData contrato)
        {
            _context.Add(contrato);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            ContratoEquipamentoData c = _context.ContratoEquipamentoData.FirstOrDefault(d => d.CodContratoEquipData == codigo);

            if (c != null)
            {
                _context.ContratoEquipamentoData.Remove(c);
                _context.SaveChanges();
            }
        }

        public ContratoEquipamentoData ObterPorCodigo(int codigo)
        {
            return _context.ContratoEquipamentoData.FirstOrDefault(c => c.CodContratoEquipData == codigo);
    }

        public PagedList<ContratoEquipamentoData> ObterPorParametros(ContratoEquipamentoDataParameters parameters)
        {
            var contratoEquipData = _context.ContratoEquipamentoData              
                .AsQueryable();
    
            if (parameters.NomeData != null)
            {
                contratoEquipData = contratoEquipData.Where(c => c.NomeData == parameters.NomeData);
            }

            if (parameters.IndAtivo != null)
            {
                contratoEquipData = contratoEquipData.Where(c => c.IndAtivo == parameters.IndAtivo);
            }

            return PagedList<ContratoEquipamentoData>.ToPagedList(contratoEquipData, parameters.PageNumber, parameters.PageSize);
        }
    }
}
