using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using System.Linq;
using System;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Repository
{
    public partial class InstalacaoAnexoRepository : IInstalacaoAnexoRepository
    {
        private readonly AppDbContext _context;

        public InstalacaoAnexoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Criar(InstalacaoAnexo instalacaoAnexo)
        {
            _context.Add(instalacaoAnexo);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            InstalacaoAnexo f = _context.InstalacaoAnexo.SingleOrDefault(f => f.CodInstalAnexo == codigo);

            if (f != null)
            {
                _context.InstalacaoAnexo.Remove(f);

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

        public InstalacaoAnexo ObterPorCodigo(int codigo)
        {
            return _context.InstalacaoAnexo.FirstOrDefault(f => f.CodInstalAnexo == codigo);
        }

        public PagedList<InstalacaoAnexo> ObterPorParametros(InstalacaoAnexoParameters parameters)
        {
            var instalacaoAnexos = _context.InstalacaoAnexo.AsQueryable();

            if (parameters.CodInstalacao.HasValue)
                instalacaoAnexos = instalacaoAnexos.Where(f => f.CodInstalacao == parameters.CodInstalacao);
            
            if (parameters.CodInstalAnexo.HasValue)
                instalacaoAnexos = instalacaoAnexos.Where(f => f.CodInstalAnexo == parameters.CodInstalAnexo);

            if (parameters.CodInstalLote.HasValue)
                instalacaoAnexos = instalacaoAnexos.Where(f => f.CodInstalLote == parameters.CodInstalLote);                                

            if (parameters.CodInstalPleito.HasValue)
                instalacaoAnexos = instalacaoAnexos.Where(f => f.CodInstalPleito == parameters.CodInstalPleito);                                                     

            if (!string.IsNullOrWhiteSpace(parameters.NomeAnexo))
                instalacaoAnexos = instalacaoAnexos.Where(f => f.NomeAnexo == parameters.NomeAnexo);

            if (!string.IsNullOrWhiteSpace(parameters.DescAnexo))
                instalacaoAnexos = instalacaoAnexos.Where(f => f.NomeAnexo == parameters.DescAnexo);                

            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                instalacaoAnexos = instalacaoAnexos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<InstalacaoAnexo>.ToPagedList(instalacaoAnexos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
