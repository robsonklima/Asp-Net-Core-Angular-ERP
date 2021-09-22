using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.INTEGRACOES.Interfaces
{
    public interface IIntegracao
    {
        void VerificaNovaIntegracao();
        void TransformaDadosIntegracao();
        void ExecutaIntegracao();
        void RetornoIntegracao();
    }
}
