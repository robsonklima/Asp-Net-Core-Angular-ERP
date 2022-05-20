using SAT.INFRA.Context;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;

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
            throw new NotImplementedException();
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