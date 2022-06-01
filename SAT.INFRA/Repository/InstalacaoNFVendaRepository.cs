using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public partial class InstalacaoNFVendaRepository : IInstalacaoNFVendaRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoNFVendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(InstalacaoNFVenda instalacaoNFVenda)
        {
            _context.ChangeTracker.Clear();
            InstalacaoNFVenda inst = _context.InstalacaoNFVenda.FirstOrDefault(i => i.CodInstalNfvenda == instalacaoNFVenda.CodInstalNfvenda);

            if (inst != null)
            {
                _context.Entry(inst).CurrentValues.SetValues(instalacaoNFVenda);
                _context.SaveChanges();
            }
        }

        public InstalacaoNFVenda Criar(InstalacaoNFVenda instalacao)
        {
            _context.Add(instalacao);
            _context.SaveChanges();

            return instalacao;
        }

        public void Deletar(int codigo)
        {
            InstalacaoNFVenda inst = _context.InstalacaoNFVenda.FirstOrDefault(i => i.CodInstalNfvenda == codigo);

            if (inst != null)
            {
                _context.InstalacaoNFVenda.Remove(inst);
                _context.SaveChanges();
            }
        }

        public InstalacaoNFVenda ObterPorCodigo(int codigo)
        {

            var data = _context.InstalacaoNFVenda

                .FirstOrDefault(i => i.CodInstalNfvenda == codigo);

            return data;
        }

        public PagedList<InstalacaoNFVenda> ObterPorParametros(InstalacaoNFVendaParameters parameters)
        {
            var instalNFVendas = _context.InstalacaoNFVenda
                .AsQueryable();

            if (parameters.Filter != null)
            {
                instalNFVendas = instalNFVendas.Where(p =>
                    p.NumNFVenda.ToString().Contains(parameters.Filter)
                );
            }

            if (parameters.CodInstalNFVenda != null)
            {
                instalNFVendas = instalNFVendas.Where(i => i.CodInstalNfvenda == parameters.CodInstalNFVenda);
            }

            if (parameters.NumNFVenda != null)
            {
                instalNFVendas = instalNFVendas.Where(i => i.NumNFVenda == parameters.NumNFVenda);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
            {
                int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                instalNFVendas = instalNFVendas.Where(os => cods.Contains(os.CodCliente));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                instalNFVendas = instalNFVendas.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<InstalacaoNFVenda>.ToPagedList(instalNFVendas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
