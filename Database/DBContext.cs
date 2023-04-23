using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_EF.Database
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=GD2015C1;TrustServerCertificate=True;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.EmplCodigo)
                    .HasName("XPKEmpleado")
                    .IsClustered(false);

                entity.ToTable("Empleado");

                entity.Property(e => e.EmplCodigo)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("empl_codigo");

                entity.Property(e => e.EmplApellido)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("empl_apellido")
                    .IsFixedLength();

                entity.Property(e => e.EmplComision)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("empl_comision");

                entity.Property(e => e.EmplDepartamento)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("empl_departamento");

                entity.Property(e => e.EmplIngreso)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("empl_ingreso");

                entity.Property(e => e.EmplJefe)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("empl_jefe");

                entity.Property(e => e.EmplNacimiento)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("empl_nacimiento");

                entity.Property(e => e.EmplNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("empl_nombre")
                    .IsFixedLength();

                entity.Property(e => e.EmplSalario)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("empl_salario");

                entity.Property(e => e.EmplTareas)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("empl_tareas")
                    .IsFixedLength();

                entity.HasOne(d => d.EmplJefeNavigation)
                    .WithMany(p => p.InverseEmplJefeNavigation)
                    .HasForeignKey(d => d.EmplJefe)
                    .HasConstraintName("FK_empleado_jefe");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.ProdCodigo)
                    .HasName("XPKProducto")
                    .IsClustered(false);

                entity.ToTable("Producto");

                entity.Property(e => e.ProdCodigo)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("prod_codigo")
                    .IsFixedLength();

                entity.Property(e => e.ProdDetalle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prod_detalle")
                    .IsFixedLength();

                entity.Property(e => e.ProdEnvase)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("prod_envase");

                entity.Property(e => e.ProdFamilia)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("prod_familia")
                    .IsFixedLength();

                entity.Property(e => e.ProdPrecio)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("prod_precio");

                entity.Property(e => e.ProdRubro)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("prod_rubro")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deletedAt");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("mail");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("password");

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .HasColumnName("rol");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.Usuario1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
