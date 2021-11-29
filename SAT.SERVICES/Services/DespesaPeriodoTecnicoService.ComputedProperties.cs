using System;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class DespesaPeriodoTecnicoService : IDespesaPeriodoTecnicoService
    {
        public DespesaPeriodoTecnico ObterClassificacaoCreditoTecnico(DespesaPeriodoTecnico despesaPeriodoTecnico)
        {
            var despesas = _despesaRepository.ObterPorParametros(new DespesaParameters
            {
                CodTecnico = despesaPeriodoTecnico.CodTecnico.ToString(),
                DataHoraInicioRAT = DateTime.Today.AddDays(-30)
            });

            var despesasTecnico = despesas
                .Where(i => i.CodTecnico == despesaPeriodoTecnico.CodTecnico);

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

            despesaPeriodoTecnico.Tecnico.TecnicoCategoriaCredito = new TecnicoCategoriaCredito
            {
                CategoriaCredito = categoria,
                Media = media,
                Valor = valor
            };

            return despesaPeriodoTecnico;
        }
    }
}