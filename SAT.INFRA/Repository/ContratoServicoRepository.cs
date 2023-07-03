using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ContratoServicoRepository : IContratoServicoRepository
    {
        private readonly AppDbContext _context;

        public ContratoServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ContratoServico contratoServico)
        {
            _context.ChangeTracker.Clear();
            ContratoServico ce = _context.ContratoServico
                .FirstOrDefault(ce => ce.CodContratoServico == contratoServico.CodContratoServico);
            try
            {
                if (ce != null)
                {
                    _context.Entry(ce).CurrentValues.SetValues(contratoServico);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(ContratoServico contratoServico)
        {
            _context.Add(contratoServico);
            _context.SaveChanges();
        }

        public void Deletar(int codContratoServico)
        {
            ContratoServico ce = _context.ContratoServico
                .FirstOrDefault(d => d.CodContratoServico == codContratoServico);

            if (ce != null)
            {
                _context.ContratoServico.Remove(ce);
                _context.SaveChanges();
            }
        }

        public ContratoServico ObterPorCodigo(int codContratoServico)
        {
            try
            {
                return _context.ContratoServico.SingleOrDefault(ce => ce.CodContratoServico == codContratoServico);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public PagedList<ContratoServico> ObterPorParametros(ContratoServicoParameters parameters)
        {
            var contratoServicos = _context.ContratoServico
                .Include(c => c.Equipamento)
                    .ThenInclude(e => e.TipoEquipamento)
                .Include(c => c.Equipamento)
                    .ThenInclude(e => e.GrupoEquipamento)
                .Include(c => c.TipoServico)
                .Include(c => c.AcordoNivelServico)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                contratoServicos = contratoServicos.Where(c => 
                    c.TipoServico.NomeServico.Contains(parameters.Filter) ||
                    c.AcordoNivelServico.NomeSLA.Contains(parameters.Filter) ||
                    c.Equipamento.NomeEquip.Contains(parameters.Filter) ||
                    c.Equipamento.DescEquip.Contains(parameters.Filter) ||
                    c.Valor.ToString().Contains(parameters.Filter)
                );
            }

            if (parameters.CodContrato != null)
            {
                contratoServicos = contratoServicos.Where(a => a.CodContrato == parameters.CodContrato);
            }

            if (parameters.CodTipoEquip != null)
            {
                contratoServicos = contratoServicos.Where(a => a.CodTipoEquip == parameters.CodTipoEquip);
            }

            if (parameters.CodGrupoEquip != null)
            {
                contratoServicos = contratoServicos.Where(a => a.CodGrupoEquip == parameters.CodGrupoEquip);
            }

            if (parameters.CodEquip != null)
            {
                contratoServicos = contratoServicos.Where(a => a.CodEquip == parameters.CodEquip);
            }

            if (parameters.CodServico != null)
            {
                contratoServicos = contratoServicos.Where(a => a.CodServico == parameters.CodServico);
            }

            if (parameters.CodSLA != null)
            {
                contratoServicos = contratoServicos.Where(a => a.CodSLA == parameters.CodSLA);
            }

            if (!string.IsNullOrEmpty(parameters.CodContratos))
            {
                var cods = parameters.CodContratos.Split(',').Select(a => int.Parse(a.Trim()));
                contratoServicos = contratoServicos.Where(e => cods.Contains(e.CodContrato));
            }

            return PagedList<ContratoServico>.ToPagedList(contratoServicos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
