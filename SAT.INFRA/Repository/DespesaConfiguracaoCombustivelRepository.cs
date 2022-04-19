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
            throw new NotImplementedException();
        }

        public void Criar(DespesaConfiguracaoCombustivel despesaConfiguracao)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaConfiguracaoCombustivel ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<DespesaConfiguracaoCombustivel> ObterPorParametros(DespesaConfiguracaoCombustivelParameters parameters)
        {
            var configuracoes =
                _context.DespesaConfiguracaoCombustivel.AsQueryable();

            if (parameters.CodFilial.HasValue)
                configuracoes = configuracoes.Where(a => a.CodFilial == parameters.CodFilial);

            if (parameters.CodUf.HasValue)
                configuracoes = configuracoes.Where(a => a.CodUf == parameters.CodUf);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                configuracoes = configuracoes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<DespesaConfiguracaoCombustivel>.ToPagedList(configuracoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
