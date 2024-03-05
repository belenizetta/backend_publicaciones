using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppPublicaciones.Models;

public partial class PublicacionContext : DbContext
{
    public PublicacionContext()
    {
    }

    public PublicacionContext(DbContextOptions<PublicacionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Publicacion> Publicacions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost; database=Publicacion; integrated security=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Publicacion>(entity =>
        {
            entity.ToTable("Publicacion");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Ambientes).HasColumnName("ambientes");
            entity.Property(e => e.Antiguedad).HasColumnName("antiguedad");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Latitud)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("latitud");
            entity.Property(e => e.ListaImagenes)
                .HasColumnType("text")
                .HasColumnName("lista_imagenes");
            entity.Property(e => e.Longitud)
                .HasColumnType("decimal(9, 6)")
                .HasColumnName("longitud");
            entity.Property(e => e.M2).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TipoOperacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_operacion");
            entity.Property(e => e.TipoPropiedad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_propiedad");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
