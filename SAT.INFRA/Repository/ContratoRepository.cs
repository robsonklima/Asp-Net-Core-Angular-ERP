using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class ContratoRepository : IContratoRepository
    {
        private readonly AppDbContext _context;

        public ContratoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Contrato contrato)
        {
            _context.ChangeTracker.Clear();
            Contrato c = _context.Contrato.FirstOrDefault(d => d.CodContrato == contrato.CodContrato);

            try
            {
                if (c != null)
                {
                    _context.Entry(c).CurrentValues.SetValues(contrato);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Criar(Contrato contrato)
        {
            _context.Add(contrato);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            Contrato c = _context.Contrato.FirstOrDefault(d => d.CodContrato == codigo);

            if (c != null)
            {
                _context.Contrato.Remove(c);
                _context.SaveChanges();
            }
        }

        public Contrato ObterPorCodigo(int codigo)
        {
            return _context.Contrato
                .Include(c => c.Cliente)            
                .Include(c => c.TipoContrato)
                .Include(c => c.Lotes)
                .FirstOrDefault(c => c.CodContrato == codigo);
        }

        public PagedList<Contrato> ObterPorParametros(ContratoParameters parameters)
        {
            var contratos = _context.Contrato
                .Include(c => c.Cliente)
                .Include(c => c.TipoContrato)
                .Include(c => c.Lotes)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                contratos = contratos.Where(
                    s =>
                    s.CodContrato.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.NroContrato.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.NomeContrato.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.Cliente.NomeFantasia.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.Cliente.CodCliente.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodContrato != null)
            {
                contratos = contratos.Where(c => c.CodContrato == parameters.CodContrato);
            }

            if (parameters.CodTipoContrato != null)
            {
                contratos = contratos.Where(c => c.TipoContrato.CodTipoContrato == parameters.CodTipoContrato);
            }

            if (parameters.IndAtivo != null)
            {
                contratos = contratos.Where(c => c.IndAtivo == parameters.IndAtivo);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
            {
                int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                contratos = contratos.Where(os => cods.Contains(os.CodCliente));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodTipoContratos))
            {
                int[] cods = parameters.CodTipoContratos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                contratos = contratos.Where(dc => cods.Contains(dc.TipoContrato.CodTipoContrato));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodContratos))
            {
                int[] cods = parameters.CodContratos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                contratos = contratos.Where(c => cods.Contains(c.CodContrato));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                contratos = contratos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Contrato>.ToPagedList(contratos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
