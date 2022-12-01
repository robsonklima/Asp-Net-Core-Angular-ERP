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
    public class MensagemTecnicoRepository : IMensagemTecnicoRepository
    {
        private readonly AppDbContext _context;

        public MensagemTecnicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(MensagemTecnico msg)
        {
            try
            {
                _context.ChangeTracker.Clear();
                MensagemTecnico m = _context.MensagemTecnico.FirstOrDefault(orc => orc.CodMensagemTecnico == msg.CodMensagemTecnico);

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

        public void Criar(MensagemTecnico MensagemTecnico)
        {
            _context.Add(MensagemTecnico);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            MensagemTecnico m = _context.MensagemTecnico.FirstOrDefault(p => p.CodMensagemTecnico == codigo);

            if (m != null)
            {
                _context.MensagemTecnico.Remove(m);
                _context.SaveChanges();
            }
        }

        public MensagemTecnico ObterPorCodigo(int codigo)
        {
            return _context.MensagemTecnico
                .Include(m => m.UsuarioDestinatario)
                .Include(m => m.UsuarioCad)
                .FirstOrDefault(p => p.CodMensagemTecnico == codigo);
        }

        public PagedList<MensagemTecnico> ObterPorParametros(MensagemTecnicoParameters parameters)
        {
            var query = _context.MensagemTecnico
                .Include(m => m.UsuarioDestinatario)
                .Include(m => m.UsuarioCad)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodMensagemTecnico.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.Assunto.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.Mensagem.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.UsuarioDestinatario.NomeUsuario.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.Assunto))
            {
                query = query.Where(m => m.Assunto.Contains(parameters.Assunto));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Mensagem))
            {
                query = query.Where(m => m.Mensagem.Contains(parameters.Mensagem));
            }

            if (parameters.IndAtivo.HasValue)
            {
                query = query.Where(m => m.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<MensagemTecnico>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
