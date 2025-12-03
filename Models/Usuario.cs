using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetoCinemanticaMVC.Models;

[Table("Usuario")]
[Index("Email", Name = "UQ__Usuario__AB6E6164080963CC", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("nome")]
    [StringLength(40)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("senha")]
    [StringLength(20)]
    [Unicode(false)]
    public byte[] SenhaHash { get; set; } = null!;
    // public byte[] Senha { get; set; } = null!;

    [Column("nick_name")]
    [StringLength(12)]
    [Unicode(false)]
    public string NickName { get; set; } = null!;

    [Column("data_nascimento")]
    public DateOnly DataNascimento { get; set; }

    [Column("desc_perfil")]
    [StringLength(100)]
    public string? DescPerfil { get; set; }

    [Column("foto_perfil")]
    public byte[]? FotoPerfil { get; set; }

    // Isso aqui vai dar merda
    // Inserido manualmente, nao veio junto com o banco de dados
    [InverseProperty("id_usuarioNavigation")]
    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
   
}
