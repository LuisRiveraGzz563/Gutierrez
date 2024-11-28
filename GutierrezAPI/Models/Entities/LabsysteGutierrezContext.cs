using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace GutierrezAPI.Models.Entities;

public partial class LabsysteGutierrezContext : DbContext
{
    public LabsysteGutierrezContext()
    {
    }

    public LabsysteGutierrezContext(DbContextOptions<LabsysteGutierrezContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Documento> Documento { get; set; }

    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<ProveedorDocumento> ProveedorDocumento { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<UsuarioProveedor> UsuarioProveedor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("documento");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.EnviarCada).HasColumnType("int(11)");
            entity.Property(e => e.Link).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Omitir).HasColumnType("tinyint(2)");
            entity.Property(e => e.SoliciarApartirDe).HasColumnName("SoliciarAPartirDe");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proveedor");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.Estado).HasColumnType("int(11)");
            entity.Property(e => e.IdDocumentos).HasColumnType("int(11)");
            entity.Property(e => e.IdProveedorServicios).HasColumnType("int(11)");
            entity.Property(e => e.IdTipoRegimen).HasColumnType("int(11)");
            entity.Property(e => e.NumRegistroRepse).HasMaxLength(45);
            entity.Property(e => e.Rfc).HasMaxLength(13);
            entity.Property(e => e.Telefono).HasColumnType("int(11)");
        });

        modelBuilder.Entity<ProveedorDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proveedor_documento");

            entity.HasIndex(e => e.IdDocumento, "fk_ProveedorDocumento_Documento");

            entity.HasIndex(e => e.IdProveedor, "fk_ProveedorDocumento_Proveedor");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdDocumento).HasColumnType("int(11)");
            entity.Property(e => e.IdProveedor).HasColumnType("int(11)");

            entity.HasOne(d => d.IdDocumentoNavigation).WithMany(p => p.ProveedorDocumento)
                .HasForeignKey(d => d.IdDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ProveedorDocumento_Documento");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProveedorDocumento)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ProveedorDocumento_Proveedor");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Correo, "Correo").IsUnique();

            entity.HasIndex(e => e.IdRol, "fk_usuario_rol_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Contraseña).HasMaxLength(128);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.IdRol).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_rol");
        });

        modelBuilder.Entity<UsuarioProveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario_proveedor");

            entity.HasIndex(e => e.IdUsuario, "usuario_proveedor_ibfk_1");

            entity.HasIndex(e => e.IdProveedor, "usuario_proveedor_ibfk_2");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdProveedor).HasColumnType("int(11)");
            entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.UsuarioProveedor)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_proveedor_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioProveedor)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_proveedor_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
