namespace Transito.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TransitoModel : DbContext
    {
        public TransitoModel()
            : base("name=TransitoModel")
        {
        }

        public virtual DbSet<Infracciones> Infracciones { get; set; }
        public virtual DbSet<Propietarios> Propietarios { get; set; }
        public virtual DbSet<Vehiculos> Vehiculos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Infracciones>()
                .Property(e => e.id_vehiculo)
                .IsUnicode(false);

            modelBuilder.Entity<Infracciones>()
                .Property(e => e.observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Propietarios>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<Propietarios>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Propietarios>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Propietarios>()
                .HasMany(e => e.Vehiculos)
                .WithRequired(e => e.Propietarios)
                .HasForeignKey(e => e.id_Propietario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehiculos>()
                .Property(e => e.placa)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculos>()
                .Property(e => e.id_Propietario)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculos>()
                .Property(e => e.marca)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculos>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculos>()
                .HasMany(e => e.Infracciones)
                .WithRequired(e => e.Vehiculos)
                .HasForeignKey(e => e.id_vehiculo)
                .WillCascadeOnDelete(false);
        }
    }
}
