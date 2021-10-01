using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class FilialRepository : IFilialRepository
    {
        private readonly AppDbContext _context;

        public FilialRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Filial filial)
        {
            throw new System.NotImplementedException();
        }

        public void Criar(Filial filial)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new System.NotImplementedException();
        }

        public Filial ObterPorCodigo(int codigo)
        {
            return _context.Filial.FirstOrDefault(f => f.CodFilial == codigo);
        }

        public PagedList<Filial> ObterPorParametros(FilialParameters parameters)
        {
            var filiais = _context.Filial
                .Include(i => i.Cidade)
                .Include(i => i.Cidade.UnidadeFederativa)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                filiais = filiais.Where(
                        f =>
                        f.CodFilial.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                        f.NomeFilial.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodFilial != null)
            {
                filiais = filiais.Where(f => f.CodFilial == parameters.CodFilial);
            }

            if (parameters.CodFiliais != null)
            {
                var paramsSplit = parameters.CodFiliais.Split(',');
                paramsSplit = paramsSplit.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var condicoes = string.Empty;

                for (int i = 0; i < paramsSplit.Length; i++)
                {
                    condicoes += string.Format("CodFilial={0}", paramsSplit[i]);
                    if (i < paramsSplit.Length - 1) condicoes += " Or ";
                }

                filiais = filiais.Where(condicoes);
            }

            if (parameters.IndAtivo != null)
            {
                filiais = filiais.Where(f => f.IndAtivo == parameters.IndAtivo);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SiglaUF))
            {
                filiais.Include(i => i.Cidade)
                        .Include(i => i.Cidade.UnidadeFederativa);

                filiais = filiais.Where(f => f.Cidade.UnidadeFederativa.SiglaUF == parameters.SiglaUF);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                filiais = filiais.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Filial>.ToPagedList(filiais, parameters.PageNumber, parameters.PageSize);
        }
    }
}
