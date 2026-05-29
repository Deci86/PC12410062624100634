using Microsoft.EntityFrameworkCore;
using PC12410062624100634.CORE.Core.Entities;

namespace PC12410062624100634.CORE.Infrastructure.Data
{
    public partial class TallerMecanicoContext : DbContext
    {
        public TallerMecanicoContext()
        {
        }

        public TallerMecanicoContext(DbContextOptions<TallerMecanicoContext> options)
            : base(options)
        {
        }

        // Mapeo de las tablas de la base de datos a los sets de entidades
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<OrdenServicio> OrdenServicios { get; set; } = null!;
        public virtual DbSet<TipoServicio> TipoServicios { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad: TipoServicio
            modelBuilder.Entity<TipoServicio>(entity =>
            {
                entity.ToTable("TipoServicio");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
                entity.Property(e => e.PrecioBase)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precioBase");
            });

            // Configuración de la entidad: Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Paterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("paterno");
                entity.Property(e => e.Materno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("materno");
                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombres");
                entity.Property(e => e.Correo)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("correo");
                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            // Configuración de la entidad: Vehiculo
            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Placa)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Modelo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.ClienteId).HasColumnName("ClienteId");

                // Relación uno a muchos: Un Cliente tiene muchos Vehículos
                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehiculo_Cliente");
            });

            // Configuración de la entidad: OrdenServicio
            modelBuilder.Entity<OrdenServicio>(entity =>
            {
                entity.ToTable("OrdenServicio");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.DescripcionProblema)
                    .IsUnicode(false);
                entity.Property(e => e.CostoEstimado)
                    .HasColumnType("decimal(10, 2)");

                // AQUÍ ESTÁ LA CORRECCIÓN CLAVE
                entity.Property(e => e.Estado)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'Ingresado'"); 

                entity.Property(e => e.VehiculoId).HasColumnName("VehiculoId");
                entity.Property(e => e.TipoServicioId).HasColumnName("TipoServicioId");

                // Relación: Una Orden pertenece a un Vehículo
                entity.HasOne(d => d.Vehiculo)
                    .WithMany(p => p.OrdenServicios)
                    .HasForeignKey(d => d.VehiculoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenServicio_Vehiculo");

                // Relación: Una Orden tiene un Tipo de Servicio asignado
                entity.HasOne(d => d.TipoServicio)
                    .WithMany(p => p.OrdenServicios)
                    .HasForeignKey(d => d.TipoServicioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdenServicio_TipoServicio");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}