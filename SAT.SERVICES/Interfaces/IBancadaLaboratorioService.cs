using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Views;

namespace SAT.SERVICES.Interfaces
{
    public interface IBancadaLaboratorioService
    {
        ListViewModel ObterPorParametros(BancadaLaboratorioParameters parameters);
        BancadaLaboratorio Criar(BancadaLaboratorio lab);
        void Deletar(int codigo);
        void Atualizar(BancadaLaboratorio lab);
        BancadaLaboratorio ObterPorCodigo(int codigo);
        public List<ViewLaboratorioTecnicoBancada> ObterTecnicosBancada();
    }
}
