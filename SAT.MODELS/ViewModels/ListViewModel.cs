using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class ListViewModel : Meta
    {
        public IEnumerable<object> Items { get; set; }
    }
}
