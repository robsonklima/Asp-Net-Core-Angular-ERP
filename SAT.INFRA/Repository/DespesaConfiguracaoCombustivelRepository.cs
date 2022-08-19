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
    public class DespesaConfiguracaoCombustivelRepository : IDespesaConfiguracaoCombustivelRepository
    {
        private readonly AppDbContext _context;

        public DespesaConfiguracaoCombustivelRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaConfiguracaoCombustivel despesaConfiguracao)
        {
            _context.ChangeTracker.Clear();
            DespesaConfiguracaoCombustivel desp = _context.DespesaConfiguracaoCombustivel.SingleOrDefault(d => d.CodDespesaConfiguracaoCombustivel == despesaConfiguracao.CodDespesaConfiguracaoCombustivel);

            if (desp != null)
            {
                try
                {
                    _context.Entry(desp).CurrentValues.SetValues(despesaConfiguracao);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }

        public void Criar(DespesaConfiguracaoCombustivel despesaConfiguracao)
        {
             try
            {
                _context.Add(despesaConfiguracao);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
             DespesaConfiguracaoCombustivel desp = _context.DespesaConfiguracaoCombustivel.SingleOrDefault(d => d.CodDespesaConfiguracaoCombustivel == codigo);

            if (desp != null)
            {
                _context.DespesaConfiguracaoCombustivel.Remove(desp);

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

        public DespesaConfiguracaoCombustivel ObterPorCodigo(int codigo)
        {
            return _context.DespesaConfiguracaoCombustivel
                .Include(d => d.Filial)
                .Include(d => d.UnidadeFederativa)
                .FirstOrDefault(d => d.CodDespesaConfiguracaoCombustivel == codigo);
        }

        public PagedList<DespesaConfiguracaoCombustivel> ObterPorParametros(DespesaConfiguracaoCombustivelParameters parameters)
        {
            var configuracoes =
                _context.DespesaConfiguracaoCombustivel
                .Include(d => d.UsuarioCadastro)
                .Include(d => d.UsuarioManutencao)
                .Include(d => d.Filial)
                .Include(d => d.UnidadeFederativa)
                .AsQueryable();

            if (parameters.CodFilial.HasValue)
                configuracoes = configuracoes.Where(a => a.CodFilial == parameters.CodFilial);

            if (parameters.CodUF.HasValue)
                configuracoes = configuracoes.Where(a => a.CodUF == parameters.CodUF);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                configuracoes = configuracoes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<DespesaConfiguracaoCombustivel>.ToPagedList(configuracoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
