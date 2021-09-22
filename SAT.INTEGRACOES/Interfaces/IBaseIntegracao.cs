
using System;

namespace SAT.INTEGRACOES.Interfaces
{
    public interface IBaseIntegracao
    {
        string NomeIntegracao { get; }

        Action eventVerificaNovaIntegracao { get; set; }
        Action eventTransformaDadosIntegracao { get; set; }
        Action eventExecutaIntegracao { get; set; }
        Action eventRetornoIntegracao { get; set; }

        void BeforeExecute();
        void AfterExecute();
    }
}
