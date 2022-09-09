using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ArquivoBanrisulRepository : IArquivoBanrisulRepository
    {
        private readonly AppDbContext _context;

        public ArquivoBanrisulRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ArquivoBanrisul arquivo)
        {
            _context.ChangeTracker.Clear();
            ArquivoBanrisul a = _context.ArquivoBanrisul
                .FirstOrDefault(a => a.CodGerenciaArquivosBanrisul == arquivo.CodGerenciaArquivosBanrisul);
            try
            {
                if (a != null)
                {
                    _context.Entry(a).CurrentValues.SetValues(arquivo);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(ArquivoBanrisul arquivo)
        {
            try
            {
                _context.Add(arquivo);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codigo)
        {
            ArquivoBanrisul a = _context.ArquivoBanrisul
                .FirstOrDefault(a => a.CodGerenciaArquivosBanrisul == codigo);

            if (a != null)
            {
                _context.ArquivoBanrisul.Remove(a);
                _context.SaveChanges();
            }
        }

        public ArquivoBanrisul ObterPorCodigo(int codigo)
        {
            try
            {
                return _context.ArquivoBanrisul
                    .Include(a => a.OrdemServico)
                    .SingleOrDefault(a => a.CodGerenciaArquivosBanrisul == codigo);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public PagedList<ArquivoBanrisul> ObterPorParametros(ArquivoBanrisulParameters parameters)
        {
            var arquivos = _context.ArquivoBanrisul
                .Include(a => a.OrdemServico)
                .AsQueryable();

            if (parameters.IndPDFGerado.HasValue) 
                arquivos = arquivos.Where(a => a.IndPDFGerado == parameters.IndPDFGerado);

            return PagedList<ArquivoBanrisul>.ToPagedList(arquivos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
