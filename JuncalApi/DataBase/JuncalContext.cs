using System;
using System.Collections.Generic;
using JuncalApi.Modelos;
using Microsoft.EntityFrameworkCore;

namespace JuncalApi.DataBase;

public partial class JuncalContext : DbContext
{
    public JuncalContext()
    {
    }

    public JuncalContext(DbContextOptions<JuncalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<JuncalAceriaMaterial> JuncalAceriaMaterials { get; set; }

    public virtual DbSet<JuncalAcerium> JuncalAceria { get; set; }

    public virtual DbSet<JuncalCamion> JuncalCamions { get; set; }

    public virtual DbSet<JuncalChofer> JuncalChofers { get; set; }

    public virtual DbSet<JuncalContrato> JuncalContratos { get; set; }

    public virtual DbSet<JuncalContratoItem> JuncalContratoItems { get; set; }

    public virtual DbSet<JuncalContratoPrecio> JuncalContratoPrecios { get; set; }

    public virtual DbSet<JuncalEstado> JuncalEstados { get; set; }

    public virtual DbSet<JuncalMaterial> JuncalMaterials { get; set; }

    public virtual DbSet<JuncalMaterialProveedor> JuncalMaterialProveedors { get; set; }

    public virtual DbSet<JuncalOrden> JuncalOrdens { get; set; }

    public virtual DbSet<JuncalOrdenMarterial> JuncalOrdenMarterials { get; set; }

    public virtual DbSet<JuncalProveedor> JuncalProveedors { get; set; }

    public virtual DbSet<JuncalRole> JuncalRoles { get; set; }

    public virtual DbSet<JuncalTransportistum> JuncalTransportista { get; set; }

    public virtual DbSet<JuncalUsuario> JuncalUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=sd-1812852-l.dattaweb.com;database=nicoales_nuevo;uid=nicoales_felix;pwd=Idra2023", ServerVersion.Parse("5.7.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<JuncalAceriaMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.aceria_material")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdAceria, "fk_aceria_materiales_aceria");

            entity.HasIndex(e => e.IdMaterial, "fk_aceria_materiales_materiales");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cod)
                .HasMaxLength(255)
                .HasColumnName("cod");
            entity.Property(e => e.IdAceria)
                .HasColumnType("int(11)")
                .HasColumnName("id_aceria");
            entity.Property(e => e.IdMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("id_material");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10)
                .HasColumnName("precio");

            entity.HasOne(d => d.IdAceriaNavigation).WithMany(p => p.JuncalAceriaMaterials)
                .HasForeignKey(d => d.IdAceria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_aceria_materiales_aceria");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.JuncalAceriaMaterials)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_aceria_materiales_materiales");
        });

        modelBuilder.Entity<JuncalAcerium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.aceria")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cuit)
                .HasMaxLength(255)
                .HasColumnName("cuit");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalCamion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.camion")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdChofer, "fk_camion_chofer");

            entity.HasIndex(e => e.IdTransportista, "fk_camion_transportista");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdChofer)
                .HasColumnType("int(11)")
                .HasColumnName("id_chofer");
            entity.Property(e => e.IdInterno)
                .HasColumnType("int(11)")
                .HasColumnName("id_interno");
            entity.Property(e => e.IdTransportista)
                .HasColumnType("int(11)")
                .HasColumnName("id_transportista");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Marca)
                .HasMaxLength(255)
                .HasColumnName("marca");
            entity.Property(e => e.Patente)
                .HasMaxLength(255)
                .HasColumnName("patente");
            entity.Property(e => e.Tara)
                .HasColumnType("int(11)")
                .HasColumnName("tara");

            entity.HasOne(d => d.IdChoferNavigation).WithMany(p => p.JuncalCamions)
                .HasForeignKey(d => d.IdChofer)
                .HasConstraintName("fk_camion_chofer");

            entity.HasOne(d => d.IdTransportistaNavigation).WithMany(p => p.JuncalCamions)
                .HasForeignKey(d => d.IdTransportista)
                .HasConstraintName("fk_camion_transportista");
        });

        modelBuilder.Entity<JuncalChofer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.chofer")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni)
                .HasColumnType("int(11)")
                .HasColumnName("dni");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<JuncalContrato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.contrato")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.IdAceria, "fk_contrato_aceria");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.FechaVigencia).HasColumnName("fecha_vigencia");
            entity.Property(e => e.IdAceria)
                .HasColumnType("int(11)")
                .HasColumnName("id_aceria");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .HasColumnName("numero");

            entity.HasOne(d => d.IdAceriaNavigation).WithMany(p => p.JuncalContratos)
                .HasForeignKey(d => d.IdAceria)
                .HasConstraintName("fk_contrato_aceria");
        });

        modelBuilder.Entity<JuncalContratoItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.contrato_items");

            entity.HasIndex(e => e.IdContrato, "fk_contrato_items_contrato");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdContrato)
                .HasColumnType("int(11)")
                .HasColumnName("id_contrato");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.JuncalContratoItems)
                .HasForeignKey(d => d.IdContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contrato_items_contrato");
        });

        modelBuilder.Entity<JuncalContratoPrecio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.contrato_precio");

            entity.HasIndex(e => e.IdMaterialAceria, "fk_contrato_precio_aceria_material");

            entity.HasIndex(e => e.IdItem, "fk_contrato_precio_contrato_items");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdItem)
                .HasColumnType("int(11)")
                .HasColumnName("id_item");
            entity.Property(e => e.IdMaterialAceria)
                .HasColumnType("int(11)")
                .HasColumnName("id_material_aceria");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Precio)
                .HasPrecision(10)
                .HasColumnName("precio");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.JuncalContratoPrecios)
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contrato_precio_contrato_items");

            entity.HasOne(d => d.IdMaterialAceriaNavigation).WithMany(p => p.JuncalContratoPrecios)
                .HasForeignKey(d => d.IdMaterialAceria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contrato_precio_aceria_material");
        });

        modelBuilder.Entity<JuncalEstado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.estados");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.material")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cod)
                .HasMaxLength(255)
                .HasColumnName("cod");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalMaterialProveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.material_proveedor");

            entity.HasIndex(e => e.IdMaterial, "fk_material_proveedor_material");

            entity.HasIndex(e => e.IdProveedor, "fk_material_proveedor_proveedor");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("id_material");
            entity.Property(e => e.IdProveedor)
                .HasColumnType("int(11)")
                .HasColumnName("id_proveedor");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Precio)
                .HasPrecision(10)
                .HasColumnName("precio");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.JuncalMaterialProveedors)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_material_proveedor_material");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.JuncalMaterialProveedors)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_material_proveedor_proveedor");
        });

        modelBuilder.Entity<JuncalOrden>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.orden");

            entity.HasIndex(e => e.IdAceria, "fk_orden_aceria");

            entity.HasIndex(e => e.IdCamion, "fk_orden_camion");

            entity.HasIndex(e => e.IdContrato, "fk_orden_contrato");

            entity.HasIndex(e => e.IdEstado, "fk_orden_estados");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdAceria)
                .HasColumnType("int(11)")
                .HasColumnName("id_aceria");
            entity.Property(e => e.IdCamion)
                .HasColumnType("int(11)")
                .HasColumnName("id_camion");
            entity.Property(e => e.IdContrato)
                .HasColumnType("int(11)")
                .HasColumnName("id_contrato");
            entity.Property(e => e.IdEstado)
                .HasColumnType("int(11)")
                .HasColumnName("id_estado");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Remito)
                .HasMaxLength(255)
                .HasColumnName("remito");

            entity.HasOne(d => d.IdAceriaNavigation).WithMany(p => p.JuncalOrdens)
                .HasForeignKey(d => d.IdAceria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orden_aceria");

            entity.HasOne(d => d.IdCamionNavigation).WithMany(p => p.JuncalOrdens)
                .HasForeignKey(d => d.IdCamion)
                .HasConstraintName("fk_orden_camion");

            entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.JuncalOrdens)
                .HasForeignKey(d => d.IdContrato)
                .HasConstraintName("fk_orden_contrato");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.JuncalOrdens)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orden_estados");
        });

        modelBuilder.Entity<JuncalOrdenMarterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.orden_marterial");

            entity.HasIndex(e => e.IdMaterial, "fk_orden_marterial_material");

            entity.HasIndex(e => e.IdOrden, "fk_orden_marterial_orden");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("id_material");
            entity.Property(e => e.IdOrden)
                .HasColumnType("int(11)")
                .HasColumnName("id_orden");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Peso)
                .HasPrecision(10)
                .HasColumnName("peso");

            entity.HasOne(d => d.IdMaterialNavigation).WithMany(p => p.JuncalOrdenMarterials)
                .HasForeignKey(d => d.IdMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orden_marterial_material");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.JuncalOrdenMarterials)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orden_marterial_orden");
        });

        modelBuilder.Entity<JuncalProveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.proveedor");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Origen)
                .HasMaxLength(255)
                .HasColumnName("origen");
        });

        modelBuilder.Entity<JuncalRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.roles");

            entity.HasIndex(e => e.Id, "unq_roles_id").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalTransportistum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("juncal.transportista")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cuit)
                .HasMaxLength(255)
                .HasColumnName("cuit");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<JuncalUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juncal.usuario");

            entity.HasIndex(e => e.IdRol, "fk_usuario_roles");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .HasColumnName("apellido");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .HasColumnName("contraseña");
            entity.Property(e => e.Dni)
                .HasColumnType("int(11)")
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IdRol)
                .HasColumnType("int(11)")
                .HasColumnName("id_rol");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Sha256)
                .HasMaxLength(255)
                .HasColumnName("sha256");
            entity.Property(e => e.Usuario)
                .HasMaxLength(255)
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.JuncalUsuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fk_usuario_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
