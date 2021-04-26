namespace Transito.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Infracciones
    {
        [Key]
        [Display(Name = "Número de infracción")]
        public int id { get; set; }

        [Required]
        [StringLength(7)]
        [Display(Name = "Placa vehiculo")]
        public string id_vehiculo { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha infracción")]
        [DataType(DataType.Date, ErrorMessage = "Solo fecha")]
        public DateTime fecha { get; set; }
        
        public int accionador { get; set; }

        [StringLength(100)]
        [Display(Name = "Observaciones")]
        public string observaciones { get; set; }

        public virtual Vehiculos Vehiculos { get; set; }
    }
}
