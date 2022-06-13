using SAT.INFRA.Context;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class IntegracaoCobraRepository : IIntegracaoCobraRepository
    {
        private readonly AppDbContext _context;

        public IntegracaoCobraRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(IntegracaoCobra integracaoCobra)
        {
            throw new NotImplementedException();
        }

        public void Criar(IntegracaoCobra integracaoCobra)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            IntegracaoCobra cobra = _context.IntegracaoCobra.SingleOrDefault(e => e.CodOS == codigo);

            if (cobra != null)
            {
                try
                {
                    _context.IntegracaoCobra.Remove(cobra);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }

        public IntegracaoCobra ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public IntegracaoCobra ObterPorCodigo(int CodOS, string NumOSCliente, string NomeTipoArquivoEnviado)
        {
            throw new NotImplementedException();
        }

        public PagedList<IntegracaoCobra> ObterPorParametros(IntegracaoCobraParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}