using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.SERVICES.Interfaces
{
    public interface IAcaoService
    {
        ListViewModel ObterPorParametros(AcaoParameters parameters);
        Acao Criar(Acao acao);
        void Deletar(int codigo);
        void Atualizar(Acao acao);
        Acao ObterPorCodigo(int codigo);
        ListViewModel ObterListaAcaoComponente(AcaoParameters parameters);
        AcaoComponente ObterAcaoComponentePorCodigo(int codAcaoComponente);
    }
}
