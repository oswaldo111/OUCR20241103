using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OUCR20241103.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Direccione = new List<Direccione>();
        }

        public int Id { get; set; }
        
        [Required(ErrorMessage = "El campo es requirido")]
        [MaxLength (50)]
        public string Nombre { get; set; } = null!;

        [Required (ErrorMessage = "El campo es requerido")]
        [MaxLength (50)]
        public string Dui { get; set; } = null!;

        public virtual IList<Direccione> Direccione { get; set; }
    }
}
