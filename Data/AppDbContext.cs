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

    public virtual DbSet<Filme> Filmes { get; set; }

    public virtual DbSet<RegraPerfil> RegraPerfils { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<diretorFilme> diretorFilmes { get; set; }

    public virtual DbSet<generoFilme> generoFilmes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.id_comentario).HasName("PK__Comentar__1BA6C6F47C585249");

            entity.HasOne(d => d.id_filmeNavigation).WithMany(p => p.Comentarios).HasConstraintName("fk_idFilme_Comentario");

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.Comentarios).HasConstraintName("FK__Comentari__id_us__5812160E");
        });

        modelBuilder.Entity<Filme>(entity =>
        {
            entity.HasKey(e => e.id_filme).HasName("PK__Filme__44A1920DC4806E9E");

            entity.HasOne(d => d.id_diretorNavigation).WithMany(p => p.Filmes).HasConstraintName("fk_idDiretor_filme");

            entity.HasOne(d => d.id_generoNavigation).WithMany(p => p.Filmes).HasConstraintName("fk_idGenero_filme");
        });

        modelBuilder.Entity<RegraPerfil>(entity =>
        {
            entity.HasKey(e => e.IdRegra).HasName("PK__RegraPer__E4F2CC24FC391EF7");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.id_usuario).HasName("PK__Usuario__4E3E04ADF40A947E");

            entity.HasOne(d => d.Regra).WithMany(p => p.Usuarios).HasConstraintName("FK_Usuario_Regra");
        });

        modelBuilder.Entity<diretorFilme>(entity =>
        {
            entity.HasKey(e => e.id_diretor).HasName("PK__diretorF__A745748E7B4B161E");
        });

        modelBuilder.Entity<generoFilme>(entity =>
        {
            entity.HasKey(e => e.id_genero).HasName("PK__generoFi__99A8E4F991954171");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
