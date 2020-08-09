using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiMysql.Models.DBModels
{
    public partial class SistemacarrosContext : DbContext
    {
        public SistemacarrosContext()
        {
        }

        public SistemacarrosContext(DbContextOptions<SistemacarrosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Datos> Datos { get; set; }
        public virtual DbSet<Eventos> Eventos { get; set; }
        public virtual DbSet<Licencia> Licencia { get; set; }
        public virtual DbSet<Tipoeventos> Tipoeventos { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=35.184.48.158;user=root;database=Sistemacarros;port=3306;password=101721", x => x.ServerVersion("5.7.25-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Datos>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("datos");

                entity.HasIndex(e => e.Idusuario)
                    .HasName("idusuario3_idx");

                entity.HasIndex(e => e.Idvehiculo)
                    .HasName("idvehiculo1_idx");

                entity.Property(e => e.Acex)
                    .IsRequired()
                    .HasColumnName("acex")
                    .HasColumnType("char(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Acey)
                    .IsRequired()
                    .HasColumnName("acey")
                    .HasColumnType("char(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Acez)
                    .IsRequired()
                    .HasColumnName("acez")
                    .HasColumnType("char(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("date");

                entity.Property(e => e.Hora)
                    .HasColumnName("hora")
                    .HasColumnType("time");

                entity.Property(e => e.Idusuario)
                    .HasColumnName("idusuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idvehiculo)
                    .HasColumnName("idvehiculo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Latitud)
                    .IsRequired()
                    .HasColumnName("latitud")
                    .HasColumnType("char(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Longitud)
                    .IsRequired()
                    .HasColumnName("longitud")
                    .HasColumnType("char(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Velocidad)
                    .IsRequired()
                    .HasColumnName("velocidad")
                    .HasColumnType("char(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idusuario3");

                entity.HasOne(d => d.IdvehiculoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idvehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idvehiculo1");
            });

            modelBuilder.Entity<Eventos>(entity =>
            {
                entity.HasKey(e => e.Ideventos)
                    .HasName("PRIMARY");

                entity.ToTable("eventos");

                entity.HasIndex(e => e.Idtipoevento)
                    .HasName("idtipoevento_idx");

                entity.HasIndex(e => e.Idusuario)
                    .HasName("idusuario2_idx");

                entity.HasIndex(e => e.Idvehiculo)
                    .HasName("idvehiculo_idx");

                entity.Property(e => e.Ideventos)
                    .HasColumnName("ideventos")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idtipoevento)
                    .HasColumnName("idtipoevento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idusuario)
                    .HasColumnName("idusuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idvehiculo)
                    .HasColumnName("idvehiculo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Puntos)
                    .IsRequired()
                    .HasColumnName("puntos")
                    .HasColumnType("char(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdtipoeventoNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.Idtipoevento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idtipoevento");

                //entity.HasOne(d => d.IdusuarioNavigation)
                //    .WithMany(p => p.Eventos)
                //    .HasForeignKey(d => d.Idusuario)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("idusuario2");

                entity.HasOne(d => d.IdvehiculoNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.Idvehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idvehiculo");
            });

            modelBuilder.Entity<Licencia>(entity =>
            {
                entity.HasKey(e => e.Idlicencia)
                    .HasName("PRIMARY");

                entity.ToTable("licencia");

                entity.HasIndex(e => e.Idusuario)
                    .HasName("idusuario _idx");

                entity.Property(e => e.Idlicencia)
                    .HasColumnName("idlicencia")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idusuario)
                    .HasColumnName("idusuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnName("tipo")
                    .HasColumnType("char(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Vence)
                    .HasColumnName("vence")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Licencia)
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idusuario ");
            });

            modelBuilder.Entity<Tipoeventos>(entity =>
            {
                entity.HasKey(e => e.Idtipoeventos)
                    .HasName("PRIMARY");

                entity.ToTable("tipoeventos");

                entity.Property(e => e.Idtipoeventos)
                    .HasColumnName("idtipoeventos")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Tipoeventos1)
                    .IsRequired()
                    .HasColumnName("tipoeventos")
                    .HasColumnType("char(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.Property(e => e.Idusuario)
                    .HasColumnName("idusuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Apellido1)
                    .IsRequired()
                    .HasColumnName("apellido1")
                    .HasColumnType("char(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Apellido2)
                    .HasColumnName("apellido2")
                    .HasColumnType("char(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasColumnName("cedula")
                    .HasColumnType("char(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasColumnName("contrasena")
                    .HasColumnType("char(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Fechanacimiento)
                    .HasColumnName("fechanacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.Nombre1)
                    .IsRequired()
                    .HasColumnName("nombre1")
                    .HasColumnType("char(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nombre2)
                    .HasColumnName("nombre2")
                    .HasColumnType("char(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.Idvehiculo)
                    .HasName("PRIMARY");

                entity.ToTable("vehiculo");

                entity.HasIndex(e => e.Idusuario1)
                    .HasName("idusuario_idx");

                entity.Property(e => e.Idvehiculo)
                    .HasColumnName("idvehiculo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idusuario1)
                    .HasColumnName("idusuario1")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Matricula)
                    .IsRequired()
                    .HasColumnName("matricula")
                    .HasColumnType("char(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Idusuario1Navigation)
                    .WithMany(p => p.Vehiculo)
                    .HasForeignKey(d => d.Idusuario1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idusuario1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
