using System;
using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Helpers;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaPeriodoTecnicoService : IDespesaPeriodoTecnicoService
    {

        public PagedList<DespesaPeriodoTecnico> ObterPropriedadesCalculadas(PagedList<DespesaPeriodoTecnico> despesasPeriodoTecnico, DespesaPeriodoTecnicoParameters parameters)
        {
            switch (parameters.FilterType)
            {
                case DespesaPeriodoTecnicoFilterEnum.FILTER_CREDITOS_CARTAO:
                    despesasPeriodoTecnico = ObterClassificacaoCreditoTecnico(despesasPeriodoTecnico);
                    break;
                default:
                    break;
            }
            return despesasPeriodoTecnico;
        }

        private PagedList<DespesaPeriodoTecnico> ObterClassificacaoCreditoTecnico(PagedList<DespesaPeriodoTecnico> despesasPeriodoTecnico)
        {
            var tecnicos = despesasPeriodoTecnico
                .Select(i => i.CodTecnico)
                .Distinct()
                .ToList();

            var despesas = _despesaRepository.ObterPorParametros(new DespesaParameters
            {
                CodTecnico = string.Join(",", tecnicos),
                DataHoraInicioRAT = DateTime.Today.AddDays(-30),
                PageSize = 1000
            }).ToArray();

            List<TecnicoCategoriaCredito> categorias =
                new List<TecnicoCategoriaCredito>();

            categorias
                .AddRange(tecnicos.Select(t => ObterClassificacaoCreditoTecnico(despesas, t)));

            despesasPeriodoTecnico.ForEach(dp =>
            {
                dp.Tecnico.TecnicoCategoriaCredito =
                    categorias.FirstOrDefault(c => c.CodTecnico == dp.CodTecnico);
            });

            return despesasPeriodoTecnico;
        }

        private TecnicoCategoriaCredito ObterClassificacaoCreditoTecnico(Despesa[] despesas, int codTecnico)
        {
            var despesasTecnico = despesas
                .Where(i => i.CodTecnico == codTecnico);

            var kmPercorrido = despesasTecnico.SelectMany(i => i.DespesaItens)
                .Where(i => i.IndAtivo == 1 && i.CodDespesaTipo == (int)DespesaTipoEnum.KM)
                .Distinct()
                .Select(i => i.KmPercorrido)
                .Sum();

            var media = kmPercorrido / 60;
            double valor;
            TecnicoCategoriaCreditoEnum categoria;

            switch (media)
            {
                case <= 60:
                    valor = 364;
                    categoria = TecnicoCategoriaCreditoEnum.D;
                    break;
                case <= 120:
                    valor = 644;
                    categoria = TecnicoCategoriaCreditoEnum.C;
                    break;
                case <= 180:
                    valor = 784;
                    categoria = TecnicoCategoriaCreditoEnum.B;
                    break;
                default:
                    valor = 1064;
                    categoria = TecnicoCategoriaCreditoEnum.A;
                    break;
            }

            return new TecnicoCategoriaCredito
            {
                CodTecnico = codTecnico,
                CategoriaCredito = categoria,
                Media = media,
                Valor = valor
            };
        }
    }
}