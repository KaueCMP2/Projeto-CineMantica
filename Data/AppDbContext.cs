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

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<diretorFilme> diretorFilmes { get; set; }

    public virtual DbSet<generoFilme> generoFilmes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.id_comentario).HasName("PK__Comentar__1BA6C6F4002C0A43");

            entity.HasOne(d => d.id_filmeNavigation).WithMany(p => p.Comentarios).HasConstraintName("FK__Comentari__id_fi__5AEE82B9");

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.Comentarios).HasConstraintName("FK__Comentari__id_us__59FA5E80");
        });

        modelBuilder.Entity<Filme>(entity =>
        {
            entity.HasKey(e => e.id_filme).HasName("PK__Filme__44A1920D562AF3B7");

            entity.HasOne(d => d.id_generoNavigation).WithMany(p => p.Filmes).HasConstraintName("FK__Filme__id_genero__5441852A");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            //por padrão veio com o "id_usuario" direto do banco de dados
            // alterado para a model Usuario.cs
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04AD4491B655");
        });

        modelBuilder.Entity<diretorFilme>(entity =>
        {
            entity.HasKey(e => e.id_diretor).HasName("PK__diretorF__A745748E0732DBF0");

            entity.HasOne(d => d.id_filmeNavigation).WithMany(p => p.diretorFilmes).HasConstraintName("FK__diretorFi__id_fi__571DF1D5");
        });

        modelBuilder.Entity<generoFilme>(entity =>
        {
            entity.HasKey(e => e.id_genero).HasName("PK__generoFi__99A8E4F974CFCD41");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
