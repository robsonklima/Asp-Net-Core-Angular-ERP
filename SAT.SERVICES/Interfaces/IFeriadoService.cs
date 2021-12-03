using System;
using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IFeriadoService
    {
        ListViewModel ObterPorParametros(FeriadoParameters parameters);
        Feriado Criar(Feriado feriado);
        void Deletar(int codigo);
        void Atualizar(Feriado feriado);
        Feriado ObterPorCodigo(int codigo);
        IEnumerable<Feriado> ObterFeriadosDoPeriodo(DateTime data);
        int ObterNroFeriadosDoPeriodo(DateTime dataInicial, DateTime dataFinal, int? codCidade, int? codUF, IEnumerable<Feriado> feriado);
        int GetDiasUteis(DateTime dataInicio, DateTime dataFim);
    }
}
