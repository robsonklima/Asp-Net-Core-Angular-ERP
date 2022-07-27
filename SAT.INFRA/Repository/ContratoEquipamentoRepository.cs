using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class ContratoEquipamentoRepository : IContratoEquipamentoRepository
    {
        private readonly AppDbContext _context;

        public ContratoEquipamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ContratoEquipamento contratoEquipamento)
        {
            _context.ChangeTracker.Clear();
            ContratoEquipamento ce = _context.ContratoEquipamento
                                                .FirstOrDefault(ce => ce.CodContrato == contratoEquipamento.CodContrato
                                                                        && ce.CodEquip == contratoEquipamento.CodEquip);
            try
            {
                if (ce != null)
                {
                    _context.Entry(ce).CurrentValues.SetValues(contratoEquipamento);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(ContratoEquipamento contratoEquipamento)
        {
            try
            {
                _context.Add(contratoEquipamento);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codContrato, int codEquip)
        {
            ContratoEquipamento ce = _context.ContratoEquipamento
                                            .FirstOrDefault(d => d.CodContrato == codContrato && d.CodEquip == codEquip);

            if (ce != null)
            {
                _context.ContratoEquipamento.Remove(ce);
                _context.SaveChanges();
            }
        }

        public ContratoEquipamento ObterPorCodigo(int codContrato, int codEquip)
        {
            try
            {
                return _context.ContratoEquipamento
                                    .SingleOrDefault(ce => ce.CodContrato == codContrato
                                                        && ce.CodEquip == codEquip);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public PagedList<ContratoEquipamento> ObterPorParametros(ContratoEquipamentoParameters parameters)
        {
            var contratoEquipamentos = _context.ContratoEquipamento
                .Include(c => c.Contrato)
                .Include(c => c.TipoEquipamento)
                .Include(c => c.GrupoEquipamento)
                .Include(c => c.Equipamento)
                .AsQueryable();

            if (parameters.CodContrato != null)
            {
                contratoEquipamentos = contratoEquipamentos.Where(a => a.CodContrato == parameters.CodContrato);
            }

            if (parameters.CodTipoEquip != null)
            {
                contratoEquipamentos = contratoEquipamentos.Where(a => a.CodTipoEquip == parameters.CodTipoEquip);
            }

            if (parameters.CodGrupoEquip != null)
            {
                contratoEquipamentos = contratoEquipamentos.Where(a => a.CodGrupoEquip == parameters.CodGrupoEquip);
            }

            if (parameters.CodEquip != null)
            {
                contratoEquipamentos = contratoEquipamentos.Where(a => a.CodGrupoEquip == parameters.CodGrupoEquip);
            }

            if (!string.IsNullOrEmpty(parameters.CodContratos))
            {
                var cods = parameters.CodContratos.Split(',').Select(a => int.Parse(a.Trim()));
                contratoEquipamentos = contratoEquipamentos.Where(e => cods.Contains(e.CodContrato));
            }

            return PagedList<ContratoEquipamento>.ToPagedList(contratoEquipamentos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
