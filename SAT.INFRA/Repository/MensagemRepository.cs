using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class MensagemRepository : IMensagemRepository
    {
        private readonly AppDbContext _context;

        public MensagemRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Mensagem msg)
        {
            try
            {
                _context.ChangeTracker.Clear();
                Mensagem m = _context.Mensagem.FirstOrDefault(orc => orc.CodMsg == msg.CodMsg);

                if (m != null)
                {
                    _context.Entry(m).CurrentValues.SetValues(msg);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Criar(Mensagem Mensagem)
        {
            _context.Add(Mensagem);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            Mensagem m = _context.Mensagem.FirstOrDefault(p => p.CodMsg == codigo);

            if (m != null)
            {
                _context.Mensagem.Remove(m);
                _context.SaveChanges();
            }
        }

        public Mensagem ObterPorCodigo(int codigo)
        {
            return _context.Mensagem
                .Include(m => m.UsuarioRemetente)
                .Include(m => m.UsuarioDestinatario)
                .FirstOrDefault(p => p.CodMsg == codigo);
        }

        public PagedList<Mensagem> ObterPorParametros(MensagemParameters parameters)
        {
            var query = _context.Mensagem
                .Include(m => m.UsuarioRemetente)
                .Include(m => m.UsuarioDestinatario)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodMsg.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.Conteudo.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Mensagem>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
