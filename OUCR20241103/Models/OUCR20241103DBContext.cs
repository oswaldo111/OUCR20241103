using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OUCR20241103.Models
{
    public partial class OUCR20241103DBContext : DbContext
    {
        public OUCR20241103DBContext()
        {
        }

        public OUCR20241103DBContext(DbContextOptions<OUCR20241103DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Direccione> Direcciones { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Direccione>(entity =>
            {
                entity.Property(e => e.Calle).HasMaxLength(50);

                entity.Property(e => e.NumeoDeCasa).HasMaxLength(50);

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Direcciones)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Direccion__IdPro__398D8EEE");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.ToTable("Proveedor");

                entity.Property(e => e.Dui).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
