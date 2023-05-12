using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System;

namespace SAT.INFRA.Repository
{
    public class EquipamentoContratoRepository : IEquipamentoContratoRepository
    {
        private AppDbContext _context;

        public EquipamentoContratoRepository(AppDbContext context)
        {
            _context = context;
        }

        public EquipamentoContrato Atualizar(EquipamentoContrato equipamentoContrato)
        {

            _context.ChangeTracker.Clear();
            EquipamentoContrato equip = _context.EquipamentoContrato.SingleOrDefault(e => e.CodEquipContrato == equipamentoContrato.CodEquipContrato);

            if (equip != null)
            {
                try
                {
                    _context.Entry(equip).CurrentValues.SetValues(equipamentoContrato);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }

            return equip;
        }

        public EquipamentoContrato Criar(EquipamentoContrato equipamentoContrato)
        {
            try
            {
                _context.Add(equipamentoContrato);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }

            return equipamentoContrato;
        }

        public void Deletar(int codigo)
        {
            EquipamentoContrato equip = _context.EquipamentoContrato.SingleOrDefault(e => e.CodEquipContrato == codigo);

            if (equip != null)
            {
                _context.EquipamentoContrato.Remove(equip);

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

        public EquipamentoContrato ObterPorCodigo(int codigo)
        {
            return _context.EquipamentoContrato
                .Include(e => e.LocalAtendimento)
                    .ThenInclude(e => e.Cidade)
                        .ThenInclude(e => e.UnidadeFederativa)
                .Include(e => e.Cliente)
                .Include(e => e.Contrato)
                    .ThenInclude(e => e.ContratosEquipamento!).DefaultIfEmpty()
                .Include(e => e.Equipamento)
                .Include(e => e.GrupoEquipamento)
                .Include(e => e.TipoEquipamento)
                .Include(e => e.RegiaoAutorizada)
                .Include(e => e.RegiaoAutorizada.Filial)
                .Include(e => e.RegiaoAutorizada.Autorizada)
                .Include(e => e.RegiaoAutorizada.Regiao)
                .FirstOrDefault(e => e.CodEquipContrato == codigo);
        }

        public PagedList<EquipamentoContrato> ObterPorParametros(EquipamentoContratoParameters parameters)
        {
            try
            {
                var equips = _context.EquipamentoContrato
                    .Include(e => e.LocalAtendimento)
                        .ThenInclude(e => e.Cidade)
                            .ThenInclude(e => e.UnidadeFederativa)
                    .Include(e => e.Cliente)
                    .Include(e => e.Contrato)
                    .ThenInclude(e => e.TipoContrato)
                    .Include(e => e.Contrato)
                        .ThenInclude(e => e.ContratosEquipamento!).DefaultIfEmpty()
                    .Include(e => e.AcordoNivelServico)
                    .Include(e => e.RegiaoAutorizada)
                        .ThenInclude(e => e.Filial)
                    .Include(e => e.RegiaoAutorizada)
                        .ThenInclude(e => e.Autorizada)
                    .Include(e => e.RegiaoAutorizada)
                        .ThenInclude(e => e.Regiao)
                    .Include(e => e.GrupoEquipamento!).DefaultIfEmpty()
                    .Include(e => e.TipoEquipamento!).DefaultIfEmpty()
                    .Include(e => e.Equipamento)
                        .ThenInclude(e => e.Equivalencia)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(parameters.Filter))
                {
                    equips = equips.Where(e =>
                        e.NumSerie.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                        e.LocalAtendimento.NomeLocal.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                        e.AtmId.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty));
                }

                if (parameters.CodEquipContrato.HasValue)
                    equips = equips.Where(e => e.CodEquipContrato == parameters.CodEquipContrato);

                if (!string.IsNullOrWhiteSpace(parameters.AtmId))
                    equips = equips.Where(e => e.AtmId.Contains(parameters.AtmId));

                if (parameters.CodPosto.HasValue)
                    equips = equips.Where(e => e.CodPosto == parameters.CodPosto);

                if (parameters.CodContrato.HasValue)
                    equips = equips.Where(e => e.CodContrato == parameters.CodContrato);

                if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
                {
                    int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                    equips = equips.Where(os => cods.Contains(os.CodCliente));
                }

                if (!string.IsNullOrWhiteSpace(parameters.CodTipoContratos))
                {
                    int[] cods = parameters.CodTipoContratos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                    equips = equips.Where(equip => cods.Contains(equip.Contrato.TipoContrato.CodTipoContrato));
                }

                if (!string.IsNullOrWhiteSpace(parameters.CodContratos))
                {
                    int[] cods = parameters.CodContratos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                    equips = equips.Where(e => cods.Contains(e.Contrato.CodContrato));
                }

                if (!string.IsNullOrEmpty(parameters.NumSerie))
                    equips = equips.Where(e => e.NumSerie.Trim().ToLower().Contains(parameters.NumSerie.Trim().ToLower()));

                if (parameters.CodFilial.HasValue)
                    equips = equips.Where(e => e.LocalAtendimento.CodFilial == parameters.CodFilial);

                if (parameters.IndAtivo.HasValue)
                    equips = equips.Where(e => e.IndAtivo == parameters.IndAtivo);

                if (!string.IsNullOrEmpty(parameters.CodPostos))
                {
                    var locais = parameters.CodPostos.Split(',').Select(f => f.Trim());
                    equips = equips.Where(e => locais.Any(p => p == e.CodPosto.ToString()));
                }

                if (!string.IsNullOrEmpty(parameters.CodCidades))
                {
                    var cidades = parameters.CodCidades.Split(',').Select(c => c.Trim());
                    equips = equips.Where(e => cidades.Any(p => p == e.LocalAtendimento.Cidade.CodCidade.ToString()));
                }

                if (!string.IsNullOrEmpty(parameters.CodUfs))
                {
                    var ufs = parameters.CodUfs.Split(',').Select(uf => uf.Trim());
                    equips = equips.Where(e => ufs.Any(p => p == e.LocalAtendimento.Cidade.CodUF.ToString()));
                }

                if (!string.IsNullOrEmpty(parameters.CodFiliais))
                {
                    var filiais = parameters.CodFiliais.Split(',').Select(f => f.Trim());
                    equips = equips.Where(e => filiais.Any(p => p == e.CodFilial.ToString()));
                }

                if (!string.IsNullOrEmpty(parameters.CodRegioes))
                {
                    var regioes = parameters.CodRegioes.Split(',').Select(f => f.Trim());
                    equips = equips.Where(e => regioes.Any(p => p == e.CodRegiao.ToString()));
                }

                if (!string.IsNullOrEmpty(parameters.CodAutorizadas))
                {
                    var autorizadas = parameters.CodAutorizadas.Split(',').Select(a => a.Trim());
                    equips = equips.Where(e => autorizadas.Any(p => p == e.CodAutorizada.ToString()));
                }

                if (!string.IsNullOrEmpty(parameters.CodTipoEquips))
                {
                    var tipos = parameters.CodTipoEquips.Split(',').Select(t => t.Trim());
                    equips = equips.Where(e => tipos.Any(p => p == e.TipoEquipamento.CodTipoEquip.ToString()));
                }

                if (!string.IsNullOrEmpty(parameters.CodEquips))
                {
                    var modelos = parameters.CodEquips.Split(',').Select(e => e.Trim());
                    equips = equips.Where(e => modelos.Any(p => p == e.Equipamento.CodEquip.ToString()));
                }

                if (!string.IsNullOrEmpty(parameters.CodGrupoEquips))
                {
                    var grupo = parameters.CodGrupoEquips.Split(',').Select(g => g.Trim());
                    equips = equips.Where(e => grupo.Any(p => p == e.GrupoEquipamento.CodGrupoEquip.ToString()));
                }

                if (!string.IsNullOrEmpty(parameters.CodEquipamentos))
                {
                    var codigos = parameters.CodEquipamentos.Split(',').Select(f => f.Trim());
                    equips = equips.Where(e => codigos.Any(p => p == e.CodEquip.ToString()));
                }

                if (!string.IsNullOrEmpty(parameters.NomeLocal))
                {
                    equips = equips.Where(e => e.LocalAtendimento.NomeLocal.Contains(parameters.NomeLocal));
                }

                if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                    equips = equips.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

                return PagedList<EquipamentoContrato>.ToPagedList(equips, parameters.PageNumber, parameters.PageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
