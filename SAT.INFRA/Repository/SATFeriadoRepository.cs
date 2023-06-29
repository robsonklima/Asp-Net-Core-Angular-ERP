﻿using SAT.INFRA.Context;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using SAT.INFRA.Interfaces;
using SAT.UTILS;

namespace SAT.INFRA.Repository
{
    public class SATFeriadoRepository : ISATFeriadoRepository
    {
        private readonly AppDbContext _context;

        public SATFeriadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public SATFeriado Atualizar(SATFeriado feriado)
        {
            _context.ChangeTracker.Clear();
            SATFeriado f = _context.SATFeriado.FirstOrDefault(f => f.CodSATFeriado == feriado.CodSATFeriado);

            if (f != null)
            {
                _context.Entry(f).CurrentValues.SetValues(feriado);
                _context.SaveChanges();
            }

            return f;
        }

        public SATFeriado Criar(SATFeriado feriado)
        {
            _context.Add(feriado);
            _context.SaveChanges();
            return feriado;
        }

        public SATFeriado Deletar(int cod)
        {
            SATFeriado f = _context.SATFeriado.FirstOrDefault(f => f.CodSATFeriado == cod);

            if (f != null)
            {
                _context.SATFeriado.Remove(f);
                _context.SaveChanges();
            }

            return f;
        }

        public SATFeriado ObterPorCodigo(int codigo)
        {
            return _context.SATFeriado.FirstOrDefault(f => f.CodSATFeriado == codigo);
        }

        public PagedList<SATFeriado> ObterPorParametros(SATFeriadoParameters parameters)
        {
            var SatFeriados = _context.SATFeriado
                .AsQueryable();

            if (parameters.Filter != null)
            {
                SatFeriados = SatFeriados.Where(
                    f =>
                    f.CodSATFeriado.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Tipo))
                SatFeriados = SatFeriados.Where(f => f.Tipo.Contains(parameters.Tipo));

            if (!string.IsNullOrWhiteSpace(parameters.UF))
                SatFeriados = SatFeriados.Where(f => f.UF.Contains(parameters.UF));       

            if (parameters.Mes.HasValue)
                SatFeriados = SatFeriados.Where(f => DataHelper.ConverterStringParaData(f.Data).Month == parameters.Mes.Value);         
            
            if (!string.IsNullOrWhiteSpace(parameters.Municipio))
                SatFeriados = SatFeriados.Where(f => f.Municipio.Contains(parameters.Municipio));

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                SatFeriados = SatFeriados.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<SATFeriado>.ToPagedList(feriados, parameters.PageNumber, parameters.PageSize);
        }
    }
}
