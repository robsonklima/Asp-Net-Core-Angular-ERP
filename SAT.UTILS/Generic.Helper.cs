using System;
using SAT.MODELS.Entities;

namespace SAT.UTILS
{
    public class GenericHelper
    {
        public static bool IsPOS(OrdemServico os)
        {
            string[] codigos = { "85", "96", "97", "107", "134", "147", "153", "157", "158", "172", "204", "268", "289", "397", "398", "399", "400", "401", "856", "1098", "1121", "1123", "1126", "1146", "1199" };

            return codigos.Contains(os.CodEquip.ToString());
        }
    }
}
