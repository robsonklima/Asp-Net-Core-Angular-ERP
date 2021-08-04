using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class ClienteListViewModel : Meta
    {
        public IEnumerable<Cliente> Clientes { get; set; }
    }
}
