using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class OrdemServicoComparer : IEqualityComparer<OrdemServico>
    {
        public bool Equals(OrdemServico x, OrdemServico y) =>
            x.CodOS.Equals(y.CodOS);

        public int GetHashCode(OrdemServico obj) =>
            obj.CodOS.GetHashCode();
    }
}