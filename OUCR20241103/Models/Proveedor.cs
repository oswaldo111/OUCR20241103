using System;
using System.Collections.Generic;

namespace OUCR20241103.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Direccione = new List<Direccione>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Dui { get; set; } = null!;

        public virtual IList<Direccione> Direccione { get; set; }
    }
}
