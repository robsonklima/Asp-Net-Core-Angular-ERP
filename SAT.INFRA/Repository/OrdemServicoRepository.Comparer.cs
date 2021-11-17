using SAT.MODELS.Entities;
using System.Collections.Generic;

namespace SAT.INFRA.Repository
{
    public class OrdemServicoComparer : IEqualityComparer<OrdemServico>
    {
        public bool Equals(OrdemServico x, OrdemServico y) =>
            x.CodOS.Equals(y.CodOS);

        public int GetHashCode(OrdemServico obj) =>
            obj.CodOS.GetHashCode();
    }
}