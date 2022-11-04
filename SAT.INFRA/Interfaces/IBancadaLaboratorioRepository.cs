using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;
using System.Collections.Generic;
using SAT.MODELS.Views;

namespace SAT.INFRA.Interfaces
{
    public interface IBancadaLaboratorioRepository
    {
        PagedList<BancadaLaboratorio> ObterPorParametros(BancadaLaboratorioParameters parameters);
        void Criar(BancadaLaboratorio lab);
        void Deletar(int codigo);
        void Atualizar(BancadaLaboratorio lab);
        BancadaLaboratorio ObterPorCodigo(int codigo);
        public List<ViewLaboratorioTecnicoBancada> ObterPorView(BancadaLaboratorioParameters parameters);
    }
}
