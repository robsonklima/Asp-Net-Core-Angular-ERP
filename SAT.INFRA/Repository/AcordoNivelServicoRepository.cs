using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using SAT.MODELS.Entities.Constants;
using System;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Repository
{
    public class AcordoNivelServicoRepository : IAcordoNivelServicoRepository
    {
        private readonly AppDbContext _context;

        public AcordoNivelServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(AcordoNivelServico acordoNivelServico)
        {
            _context.ChangeTracker.Clear();
            AcordoNivelServico ans = _context.AcordoNivelServico.SingleOrDefault(a => a.CodSLA == acordoNivelServico.CodSLA);

            if (ans != null)
            {
                try
                {
                    _context.Entry(ans).CurrentValues.SetValues(acordoNivelServico);
                    _context.Entry(ans).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(AcordoNivelServico acordoNivelServico)
        {
            try
            {
                _context.Add(acordoNivelServico);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            AcordoNivelServico ans = _context.AcordoNivelServico.SingleOrDefault(a => a.CodSLA == codigo);

            if (ans != null)
            {
                _context.AcordoNivelServico.Remove(ans);

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

        /// <summary>
        ///  SLA Legado Atualizar
        /// </summary>
        public void AtualizarLegado(AcordoNivelServicoLegado acordoNivelServicoLegado)
        {
            _context.ChangeTracker.Clear();
            AcordoNivelServicoLegado ans = _context.AcordoNivelServicoLegado.SingleOrDefault(a => a.CodSla == acordoNivelServicoLegado.CodSla);

            if (ans != null)
            {
                try
                {
                    _context.Entry(ans).CurrentValues.SetValues(acordoNivelServicoLegado);
                    _context.Entry(ans).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        /// <summary>
        ///  SLA Legado Criar
        /// </summary>
        public void CriarLegado(AcordoNivelServicoLegado acordoNivelServicoLegado)
        {
            try
            {
                _context.Add(acordoNivelServicoLegado);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        /// <summary>
        ///  SLA Legado Deletar
        /// </summary>
        public void DeletarLegado(int codigo)
        {
            AcordoNivelServicoLegado ans = _context.AcordoNivelServicoLegado.SingleOrDefault(a => a.CodSla == codigo);

            if (ans != null)
            {
                _context.AcordoNivelServicoLegado.Remove(ans);
                
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

        public AcordoNivelServico ObterPorCodigo(int codigo)
        {
            return _context.AcordoNivelServico.SingleOrDefault(a => a.CodSLA == codigo);
        }

        public PagedList<AcordoNivelServico> ObterPorParametros(AcordoNivelServicoParameters parameters)
        {
            var slas = _context.AcordoNivelServico.AsQueryable();

            if (parameters.Filter != null)
            {
                slas = slas.Where(
                            s =>
                            s.NomeSLA.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            s.CodSLA.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.NomeSLA))
            {
               slas = slas.Where(r => r.NomeSLA == parameters.NomeSLA);
            }                       

            if (parameters.CodSLA != null)
            {
                slas = slas.Where(s => s.CodSLA == parameters.CodSLA);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                slas = slas.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<AcordoNivelServico>.ToPagedList(slas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
