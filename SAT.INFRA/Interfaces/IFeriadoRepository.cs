using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;

namespace SAT.INFRA.Interfaces
{
    public interface IFeriadoRepository
    {
        PagedList<Feriado> ObterPorParametros(FeriadoParameters parameters);
        void Criar(Feriado feriado);
        void Atualizar(Feriado feriado);
        void Deletar(int codFeriado);
        Feriado ObterPorCodigo(int codigo);
        int CalculaDiasNaoUteis(DateTime dataInicio, DateTime dataFim, bool contabilizarSabado = false, bool contabilizarDomingo = false, bool contabilizarFeriados = false, int? codCidade = null);
    }
}
