using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnicoComparer : IEqualityComparer<AgendaTecnico>
    {
        public bool Equals(AgendaTecnico x, AgendaTecnico y) =>
            x.CodOS.Equals(y.CodOS) && x.CodTecnico.Equals(y.CodTecnico);

        public int GetHashCode(AgendaTecnico obj) =>
            new { A = obj.CodOS, B = obj.CodTecnico }.GetHashCode();
    }
}