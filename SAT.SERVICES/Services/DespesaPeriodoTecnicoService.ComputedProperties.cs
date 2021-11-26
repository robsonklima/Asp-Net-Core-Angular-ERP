using System;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Helpers;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaPeriodoTecnicoService : IDespesaPeriodoTecnicoService
    {
        public PagedList<DespesaPeriodoTecnico> ComputarPropriedades(PagedList<DespesaPeriodoTecnico> result, DespesaPeriodoTecnicoParameters parameters)
        {
            switch (parameters.FilterType)
            {
                case MODELS.Enums.DespesaPeriodoTecnicoFilterEnum.FILTER_CREDITOS_CARTAO:
                    result = ClassificaTecnicos(result, parameters);
                    break;
                default:
                    break;
            }

            return result;
        }

        private PagedList<DespesaPeriodoTecnico> ClassificaTecnicos(PagedList<DespesaPeriodoTecnico> result, DespesaPeriodoTecnicoParameters parameters)
        {
            var codTecnicos = string.Join(",", result
                .Where(i => i.Tecnico != null).Select(i => i.CodTecnico));

            var despesas = _despesaRepository.ObterPorParametros(new DespesaParameters
            {
                CodTecnico = codTecnicos,
                DataHoraInicioRAT = DateTime.Today.AddDays(-30)
            });

            result
                .Where(i => i.Tecnico != null)
                .ToList()
                .ForEach(i => i.Tecnico.TecnicoCategoriaCredito =
                    ClassificaTecnico(i.Tecnico, despesas.ToArray()));

            return result;
        }

        private TecnicoCategoriaCredito ClassificaTecnico(Tecnico tecnico, Despesa[] despesas)
        {
            var despesasTecnico = despesas
                .Where(i => i.CodTecnico == tecnico.CodTecnico);

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
                CategoriaCredito = categoria,
                Media = media,
                Valor = valor
            };
        }
    }
}