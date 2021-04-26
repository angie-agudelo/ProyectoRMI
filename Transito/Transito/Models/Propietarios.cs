namespace Transito.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Propietarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Propietarios()
        {
            Vehiculos = new HashSet<Vehiculos>();
        }

        [StringLength(15)]
        [Key]
        [Required(ErrorMessage = "El {0} es Requerido")]
        [MinLength(5, ErrorMessage = "El {0} debe ser Minimo de {1} caracteres")]
        [Display(Name = "Número de Identificación")]
        [DataType(DataType.Text)]
        [RegularExpression(@"[a-zA-Z0-9]*$", ErrorMessage = "Caracteres no Validos para {0}")]
        public string id { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido")]
        [Display(Name = "Tipo de Identificación")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Debe seleccionar un valor")]
        public int tipo_Id { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "El {0} es Requerido")]
        [MinLength(10, ErrorMessage = "El {0} debe ser Minimo de {1} caracteres")]
        [Display(Name = "Nombre completo")]
        [DataType(DataType.Text)]
        [RegularExpression(@"[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]*$", ErrorMessage = "Caracteres no Validos para {0}")]
        public string nombre { get; set; }

        [StringLength(100)]
        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehiculos> Vehiculos { get; set; }
    }
}
