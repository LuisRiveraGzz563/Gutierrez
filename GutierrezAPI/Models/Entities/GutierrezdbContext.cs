using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace GutierrezAPI.Models.Entities;

public partial class GutierrezdbContext : DbContext
{
    public GutierrezdbContext()
    {
    }

    public GutierrezdbContext(DbContextOptions<GutierrezdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Documento> Documento { get; set; }

    public virtual DbSet<Grupo> Grupo { get; set; }

    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<ProveedorDocumento> ProveedorDocumento { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<UsuarioProveedor> UsuarioProveedor { get; set; }

    public virtual DbSet<Usuariogrupo> Usuariogrupo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=gutierrezdb;username=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.39-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("documento");

            entity.Property(e => e.EnviarCada).HasMaxLength(30);
            entity.Property(e => e.Link).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.SoliciarApartirDe)
                .HasMaxLength(50)
                .HasColumnName("SoliciarAPartirDe");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("grupo");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proveedor");

            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.NumRegistroRepse).HasMaxLength(45);
            entity.Property(e => e.Rfc).HasMaxLength(13);
        });

        modelBuilder.Entity<ProveedorDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proveedor_documento");

            entity.HasIndex(e => e.IdDocumento, "fk_ProveedorDocumento_Documento_idx");

            entity.HasIndex(e => e.IdProveedor, "fk_ProveedorDocumento_Proveedor_idx");

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

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Correo, "Correo_UNIQUE").IsUnique();

            entity.HasIndex(e => e.IdRol, "fk_usuario_rol_idx");

            entity.Property(e => e.Contraseña).HasMaxLength(128);
            entity.Property(e => e.Correo).HasMaxLength(100);
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

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.UsuarioProveedor)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_proveedor_ibfk_2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioProveedor)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_proveedor_ibfk_1");
        });

        modelBuilder.Entity<Usuariogrupo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuariogrupo");

            entity.HasIndex(e => e.IdGrupo, "fk_UsuarioGrupo_Grupo");

            entity.HasIndex(e => e.IdUsuario, "fk_UsuarioGrupo_Usuario");

            entity.HasOne(d => d.IdGrupoNavigation).WithMany(p => p.Usuariogrupo)
                .HasForeignKey(d => d.IdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UsuarioGrupo_Grupo");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Usuariogrupo)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UsuarioGrupo_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
