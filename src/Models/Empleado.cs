using System;
using System.Collections.Generic;

namespace API_EF.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            InverseEmplJefeNavigation = new HashSet<Empleado>();
        }

        public decimal EmplCodigo { get; set; }
        public string EmplNombre { get; set; }
        public string EmplApellido { get; set; }
        public DateTime? EmplNacimiento { get; set; }
        public DateTime? EmplIngreso { get; set; }
        public string EmplTareas { get; set; }
        public decimal? EmplSalario { get; set; }
        public decimal? EmplComision { get; set; }
        public decimal? EmplJefe { get; set; }
        public decimal? EmplDepartamento { get; set; }

        public virtual Empleado EmplJefeNavigation { get; set; }
        public virtual ICollection<Empleado> InverseEmplJefeNavigation { get; set; }
    }
}
