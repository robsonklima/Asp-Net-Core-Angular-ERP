using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class LocalAtendimentoRepository : ILocalAtendimentoRepository
    {
        private readonly AppDbContext _context;

        public LocalAtendimentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(LocalAtendimento localAtendimento)
        {
            _context.ChangeTracker.Clear();
            LocalAtendimento local = _context.LocalAtendimento.SingleOrDefault(l => l.CodPosto == localAtendimento.CodPosto);
            
            if (local != null)
            {
                try
                {
                    _context.Entry(local).CurrentValues.SetValues(localAtendimento);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }

        public void Criar(LocalAtendimento localAtendimento)
        {
            try
            {
                _context.Add(localAtendimento);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            var localAtendimento = _context.LocalAtendimento.SingleOrDefault(l => l.CodPosto == codigo);

            if (localAtendimento != null)
            {
                _context.LocalAtendimento.Remove(localAtendimento);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public LocalAtendimento ObterPorCodigo(int codigo)
        {
            return _context.LocalAtendimento
                .Include(l => l.Cidade)
                .Include(l => l.Cidade.UnidadeFederativa)
                .Include(l => l.Cidade.UnidadeFederativa.Pais)
                .Include(l => l.Cliente)
                .Include(l => l.TipoRota)
                .Include(l => l.Filial)
                .FirstOrDefault(f => f.CodPosto == codigo);
        }

        public PagedList<LocalAtendimento> ObterPorParametros(LocalAtendimentoParameters parameters)
        {
            var locais = _context.LocalAtendimento
                .Include(l => l.Filial)
                .Include(l => l.Cliente)
                .Include(l => l.Cidade)
                .Include(l => l.TipoRota)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter)) 
            {
                if (parameters.Filter.Contains("/")) 
                {
                    var agenciaPosto = parameters.Filter.Split("/");

                    if (agenciaPosto.Count() > 1) {
                        string agencia = agenciaPosto[0];
                        string posto = agenciaPosto[1];

                        if (!string.IsNullOrWhiteSpace(agencia))
                            locais = locais.Where(l => l.NumAgencia == agencia);

                        if (!string.IsNullOrWhiteSpace(posto))
                            locais = locais.Where(l => l.DCPosto == posto);
                    }
                } 
                else
                {
                    locais = locais.Where(
                        l =>
                        l.Cliente.NomeFantasia.Contains(parameters.Filter) ||
                        l.CodPosto.ToString().Contains(parameters.Filter) ||
                        l.NomeLocal.Contains(parameters.Filter) ||
                        l.DCPosto.Contains(parameters.Filter) ||
                        l.NumAgencia.Contains(parameters.Filter)
                    );
                }
            }

            if (parameters.CodPosto.HasValue)
                locais = locais.Where(l => l.CodPosto == parameters.CodPosto);

            if (parameters.CodCliente.HasValue)
                locais = locais.Where(l => l.CodCliente == parameters.CodCliente);

            if (!string.IsNullOrWhiteSpace(parameters.NumAgencia))
                locais = locais.Where(l => l.NumAgencia == parameters.NumAgencia);

            if (!string.IsNullOrWhiteSpace(parameters.DCPosto))
                locais = locais.Where(l => l.DCPosto == parameters.DCPosto);

            if (parameters.CodFilial.HasValue)
                locais = locais.Where(l => l.CodFilial == parameters.CodFilial);

            if (parameters.IndAtivo.HasValue)
                locais = locais.Where(l => l.IndAtivo == parameters.IndAtivo);

            if (!string.IsNullOrWhiteSpace(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                locais = locais.Where(l => cods.Contains(l.CodFilial.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
            {
                int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                locais = locais.Where(l => cods.Contains(l.CodCliente));
            }

            if (!string.IsNullOrWhiteSpace(parameters.DCPostoNotIn))
            {
                string[] cods = parameters.DCPosto.Split(",").Select(a => a.Trim()).Distinct().ToArray();
                locais = locais.Where(l => !cods.Contains(l.DCPosto));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                locais = locais.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<LocalAtendimento>.ToPagedList(locais, parameters.PageNumber, parameters.PageSize);
        }
    }
}
