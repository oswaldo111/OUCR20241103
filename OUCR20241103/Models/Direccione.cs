using System;
using System.Collections.Generic;

namespace OUCR20241103.Models
{
    public partial class Direccione
    {
        public int Id { get; set; }
        public string Calle { get; set; } = null!;
        public string NumeoDeCasa { get; set; } = null!;
        public int? IdProveedor { get; set; }

        public virtual Proveedor? IdProveedorNavigation { get; set; }
    }
}
