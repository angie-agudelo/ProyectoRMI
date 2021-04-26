namespace Transito.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vehiculos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehiculos()
        {
            Infracciones = new HashSet<Infracciones>();
        }

        [Key]
        [StringLength(7)]
        [Display(Name = "Placa")]
        [Required(ErrorMessage = "El {0} es Requerido")]
        public string placa { get; set; }

        [Required]
        [StringLength(15)]
        [Range(1, Int32.MaxValue, ErrorMessage = "Debe seleccionar un valor")]
        [Display(Name = "Propietario")]

        public string id_Propietario { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Marca")]
        public string marca { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date, ErrorMessage = "Solo fecha")]
        public DateTime fecha { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tipo")]
        public string tipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Infracciones> Infracciones { get; set; }

        public virtual Propietarios Propietarios { get; set; }
    }
}
