using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace SAT.INFRA.Repository
{
    public partial class ClientePecaRepository : IClientePecaRepository
    {
        private readonly AppDbContext _context;

        public ClientePecaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ClientePeca clientePeca)
        {
            _context.ChangeTracker.Clear();
            ClientePeca p = _context.ClientePeca.FirstOrDefault(p => p.CodClientePeca == clientePeca.CodClientePeca);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(clientePeca);
                _context.Entry(p).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Criar(ClientePeca clientePeca)
        {
            try
            {
                _context.Add(clientePeca);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codClientePeca)
        {
            ClientePeca p = _context.ClientePeca.FirstOrDefault(p => p.CodClientePeca == codClientePeca);

            if (p != null)
            {
                _context.ClientePeca.Remove(p);
                _context.SaveChanges();
            }
        }

        //Peca já tem lista de ClientePeca, acontece erro se usar include direto
        private IQueryable<ClientePeca> MontaClientePecaQuery()
        {
            return (from c in _context.ClientePeca
                    join p in _context.Peca on c.CodPeca equals p.CodPeca into joinPeca
                    from peca in joinPeca.DefaultIfEmpty()
                    join cl in _context.Cliente on c.CodCliente equals cl.CodCliente into joinCliente
                    from cliente in joinCliente.DefaultIfEmpty()
                    join co in _context.Contrato on c.CodContrato equals co.CodContrato into joinContrato
                    from contrato in joinContrato.DefaultIfEmpty()
                    select new ClientePeca
                    {
                        CodCliente = c.CodCliente,
                        CodClientePeca = c.CodClientePeca,
                        CodContrato = c.CodContrato,
                        CodPeca = c.CodPeca,
                        CodUsuarioCad = c.CodUsuarioCad,
                        CodUsuarioManut = c.CodUsuarioManut,
                        DataHoraCad = c.DataHoraCad,
                        DataHoraManut = c.DataHoraManut,
                        ValorIPI = c.ValorIPI,
                        ValorUnitario = c.ValorUnitario,
                        VlrBaseTroca = c.VlrBaseTroca,
                        VlrSubstituicaoNovo = c.VlrSubstituicaoNovo,
                        Peca = peca,
                        Cliente = cliente,
                        Contrato = contrato
                    })
                    .AsQueryable();
        }

        public ClientePeca ObterPorCodigo(int codigo)
        {
            return this.MontaClientePecaQuery()
                  .FirstOrDefault(p => p.CodClientePeca == codigo);
        }

        public IQueryable<ClientePeca> ObterQuery(ClientePecaParameters parameters)
        {
            var query = this.MontaClientePecaQuery();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    c =>
                    c.Peca.NomePeca.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.CodPeca.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    c.CodCliente.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodClientePeca.HasValue)
            {
                query = query.Where(a => a.CodClientePeca == parameters.CodClientePeca);
            }

            if (parameters.CodContrato.HasValue)
            {
                query = query.Where(a => a.CodContrato == parameters.CodContrato);
            }

            if (parameters.CodMagnus != null)
            {
                query = query.Where(a => a.Peca.CodMagnus == parameters.CodMagnus);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return query;
        }

        public PagedList<ClientePeca> ObterPorParametros(ClientePecaParameters parameters)
        {
            var query = this.ObterQuery(parameters);

            if (!string.IsNullOrWhiteSpace(parameters.CodPecaStatus))
            {
                int[] cods = parameters.CodPecaStatus.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.Peca.PecaStatus.CodPecaStatus));
            }
            
            if (!string.IsNullOrWhiteSpace(parameters.CodContratos))
            {
                int[] cods = parameters.CodContratos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.Contrato.CodContrato));
            }
            
            if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
            {
                int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.Cliente.CodCliente));
            }                        

            return PagedList<ClientePeca>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}