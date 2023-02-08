using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOSBancadaPecasRepository
    {
        PagedList<OSBancadaPecas> ObterPorParametros(OSBancadaPecasParameters parameters);
        void Criar(OSBancadaPecas osBancadaPecas);
        void Atualizar(OSBancadaPecas osBancadaPecas);
        void Deletar(int CodOsbancada, int codPecaRe5114);
    }
}
