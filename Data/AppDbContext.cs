using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjetoCinemanticaMVC.Models;

namespace ProjetoCinemanticaMVC.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Seguindo> Seguindos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.id_comentario).HasName("PK__Comentar__1BA6C6F47C585249");

            entity.Property(e => e.data_post).HasDefaultValueSql("(dateadd(hour,(-3),sysutcdatetime()))");

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.Comentarios).HasConstraintName("FK__Comentari__id_us__5812160E");
        });

        modelBuilder.Entity<Seguindo>(entity =>
        {
            entity.HasKey(e => e.id_seguindo).HasName("PK__Seguindo__7862076163DC43C5");

            entity.Property(e => e.data_seguindo).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.seguidor).WithMany(p => p.Seguindoseguidors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Seguindo_Seguidor");

            entity.HasOne(d => d.seguindo).WithMany(p => p.Seguindoseguindos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Seguindo_Seguido");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.id_usuario).HasName("PK__Usuario__4E3E04ADF40A947E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
